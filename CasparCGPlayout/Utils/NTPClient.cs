using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace CasparCGPlayout.Utils
{
    /// <summary>
    /// NTPClient is a C# class designed to connect to time servers on the Internet.
    /// The implementation of the protocol is based on the RFC 2030.
    /// 
    /// Public class members:
    ///
    /// LeapIndicator - Warns of an impending leap second to be inserted/deleted in the last
    /// minute of the current day. (See the _LeapIndicator enum)
    /// 
    /// VersionNumber - Version number of the protocol (3 or 4).
    /// 
    /// Mode - Returns mode. (See the _Mode enum)
    /// 
    /// Stratum - Stratum of the clock. (See the _Stratum enum)
    /// 
    /// PollInterval - Maximum interval between successive messages.
    /// 
    /// Precision - Precision of the clock.
    /// 
    /// RootDelay - Round trip time to the primary reference source.
    /// 
    /// RootDispersion - Nominal error relative to the primary reference source.
    /// 
    /// ReferenceID - Reference identifier (either a 4 character string or an IP address).
    /// 
    /// ReferenceTimestamp - The time at which the clock was last set or corrected.
    /// 
    /// OriginateTimestamp - The time at which the request departed the client for the server.
    /// 
    /// ReceiveTimestamp - The time at which the request arrived at the server.
    /// 
    /// Transmit Timestamp - The time at which the reply departed the server for client.
    /// 
    /// RoundTripDelay - The time between the departure of request and arrival of reply.
    /// 
    /// LocalClockOffset - The offset of the local clock relative to the primary reference
    /// source.
    /// 
    /// Initialize - Sets up data structure and prepares for connection.
    /// 
    /// Connect - Connects to the time server and populates the data structure.
    ///     It can also set the system time.
    /// 
    /// IsResponseValid - Returns true if received data is valid and if comes from
    /// a NTP-compliant time server.
    /// 
    /// ToString - Returns a string representation of the object.
    /// 
    /// -----------------------------------------------------------------------------
    /// Structure of the standard NTP header (as described in RFC 2030)
    ///                       1                   2                   3
    ///   0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |LI | VN  |Mode |    Stratum    |     Poll      |   Precision   |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                          Root Delay                           |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                       Root Dispersion                         |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                     Reference Identifier                      |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                                                               |
    ///  |                   Reference Timestamp (64)                    |
    ///  |                                                               |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                                                               |
    ///  |                   Originate Timestamp (64)                    |
    ///  |                                                               |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                                                               |
    ///  |                    Receive Timestamp (64)                     |
    ///  |                                                               |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                                                               |
    ///  |                    Transmit Timestamp (64)                    |
    ///  |                                                               |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                 Key Identifier (optional) (32)                |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///  |                                                               |
    ///  |                                                               |
    ///  |                 Message Digest (optional) (128)               |
    ///  |                                                               |
    ///  |                                                               |
    ///  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /// 
    /// -----------------------------------------------------------------------------
    /// 
    /// NTP Timestamp Format (as described in RFC 2030)
    ///                         1                   2                   3
    ///     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /// |                           Seconds                             |
    /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /// |                  Seconds Fraction (0-padded)                  |
    /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /// 
    /// </summary>
    public class NTPClient
    {
        // NTP Data Structure Length
        private const byte NTPDataLength = 48;
        // NTP Data Structure (as described in RFC 2030)

        // Offset constants for timestamps in the data structure
        private const byte OffReferenceId = 12;
        private const byte OffReferenceTimestamp = 16;
        private const byte OffOriginateTimestamp = 24;
        private const byte OffReceiveTimestamp = 32;
        private const byte OffTransmitTimestamp = 40;
        private readonly string _timeServer;
        public DateTime ReceptionTimestamp;
        private byte[] _ntpData = new byte[NTPDataLength];

        public NTPClient(string host)
        {
            _timeServer = host;
        }

        // Leap Indicator
        public LeapIndicator leapIndicator
        {
            get
            {
                // Isolate the two most significant bits
                var val = (byte) (_ntpData[0] >> 6);
                switch (val)
                {
                    case 0:
                        return LeapIndicator.NoWarning;
                    case 1:
                        return LeapIndicator.LastMinute61;
                    case 2:
                        return LeapIndicator.LastMinute59;
                    case 3:
                        goto default;
                    default:
                        return LeapIndicator.Alarm;
                }
            }
        }

        // Version Number
        public byte versionNumber
        {
            get
            {
                // Isolate bits 3 - 5
                var val = (byte) ((_ntpData[0] & 0x38) >> 3);
                return val;
            }
        }

        // Mode
        public Mode mode
        {
            get
            {
                // Isolate bits 0 - 3
                var val = (byte) (_ntpData[0] & 0x7);
                switch (val)
                {
                    case 0:
                        goto default;
                    case 6:
                        goto default;
                    case 7:
                        goto default;
                    default:
                        return Mode.Unknown;
                    case 1:
                        return Mode.SymmetricActive;
                    case 2:
                        return Mode.SymmetricPassive;
                    case 3:
                        return Mode.Client;
                    case 4:
                        return Mode.Server;
                    case 5:
                        return Mode.Broadcast;
                }
            }
        }

        // Stratum
        public Stratum stratum
        {
            get
            {
                byte val = _ntpData[1];
                if (val == 0) return Stratum.Unspecified;
                if (val == 1) return Stratum.PrimaryReference;
                if (val <= 15) return Stratum.SecondaryReference;
                return Stratum.Reserved;
            }
        }

        // Poll Interval
        public uint pollInterval
        {
            get { return (uint) Math.Round(Math.Pow(2, _ntpData[2])); }
        }

        // Precision (in milliseconds)
        public double precision
        {
            get { return (1000*Math.Pow(2, _ntpData[3])); }
        }

        // Root Delay (in milliseconds)
        public double rootDelay
        {
            get
            {
                int temp = 256*(256*(256*_ntpData[4] + _ntpData[5]) + _ntpData[6]) + _ntpData[7];
                return 1000*(((double) temp)/0x10000);
            }
        }

        // Root Dispersion (in milliseconds)
        public double rootDispersion
        {
            get
            {
                int temp = 256*(256*(256*_ntpData[8] + _ntpData[9]) + _ntpData[10]) + _ntpData[11];
                return 1000*(((double) temp)/0x10000);
            }
        }

        // Reference Identifier
        public string referenceId
        {
            get
            {
                string val = "";
                switch (stratum)
                {
                    case Stratum.Unspecified:
                        goto case Stratum.PrimaryReference;
                    case Stratum.PrimaryReference:
                        val += (char) _ntpData[OffReferenceId + 0];
                        val += (char) _ntpData[OffReferenceId + 1];
                        val += (char) _ntpData[OffReferenceId + 2];
                        val += (char) _ntpData[OffReferenceId + 3];
                        break;
                    case Stratum.SecondaryReference:
                        switch (versionNumber)
                        {
                            case 3: // Version 3, Reference ID is an IPv4 address
                                string address = _ntpData[OffReferenceId + 0].ToString(CultureInfo.InvariantCulture) + "." +
                                                 _ntpData[OffReferenceId + 1].ToString(CultureInfo.InvariantCulture) + "." +
                                                 _ntpData[OffReferenceId + 2].ToString(CultureInfo.InvariantCulture) + "." +
                                                 _ntpData[OffReferenceId + 3].ToString(CultureInfo.InvariantCulture);
                                try
                                {
#pragma warning disable 612,618
                                    IPHostEntry host = Dns.GetHostByAddress(address);
#pragma warning restore 612,618
                                    val = host.HostName + " (" + address + ")";
                                }
                                catch (Exception)
                                {
                                    val = "N/A";
                                }
                                break;
                            case 4: // Version 4, Reference ID is the timestamp of last update
                                DateTime time = ComputeDate(getMilliSeconds(OffReferenceId));
                                // Take care of the time zone
                                TimeSpan offspan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                                val = (time + offspan).ToString(CultureInfo.InvariantCulture);
                                break;
                            default:
                                val = "N/A";
                                break;
                        }
                        break;
                }

                return val;
            }
        }

        // Reference Timestamp
        public DateTime referenceTimestamp
        {
            get
            {
                DateTime time = ComputeDate(getMilliSeconds(OffReferenceTimestamp));
                // Take care of the time zone
                TimeSpan offspan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                return time + offspan;
            }
        }

        // Originate Timestamp
        public DateTime originateTimestamp
        {
            get { return ComputeDate(getMilliSeconds(OffOriginateTimestamp)); }
        }

        // Receive Timestamp
        public DateTime receiveTimestamp
        {
            get
            {
                DateTime time = ComputeDate(getMilliSeconds(OffReceiveTimestamp));
                // Take care of the time zone
                TimeSpan offspan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                return time + offspan;
            }
        }

        // Transmit Timestamp
        public DateTime transmitTimestamp
        {
            get
            {
                DateTime time = ComputeDate(getMilliSeconds(OffTransmitTimestamp));
                // Take care of the time zone
                TimeSpan offspan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                return time + offspan;
            }
            set { setDate(OffTransmitTimestamp, value); }
        }

        // Reception Timestamp

        // Round trip delay (in milliseconds)
        public int roundTripDelay
        {
            get
            {
                TimeSpan span = (receiveTimestamp - originateTimestamp) + (ReceptionTimestamp - transmitTimestamp);
                return (int) span.TotalMilliseconds;
            }
        }

        // Local clock offset (in milliseconds)
        public int localClockOffset
        {
            get
            {
                TimeSpan span = (receiveTimestamp - originateTimestamp) - (ReceptionTimestamp - transmitTimestamp);
                return (int) (span.TotalMilliseconds/2);
            }
        }

        // Compute date, given the number of milliseconds since January 1, 1900
        private DateTime ComputeDate(ulong milliseconds)
        {
            TimeSpan span = TimeSpan.FromMilliseconds(milliseconds);
            var time = new DateTime(1900, 1, 1);
            time += span;
            return time;
        }

        // Compute the number of milliseconds, given the offset of a 8-byte array
        private ulong getMilliSeconds(byte offset)
        {
            ulong intpart = 0, fractpart = 0;

            for (int i = 0; i <= 3; i++)
            {
                intpart = 256*intpart + _ntpData[offset + i];
            }
            for (int i = 4; i <= 7; i++)
            {
                fractpart = 256*fractpart + _ntpData[offset + i];
            }
            ulong milliseconds = intpart*1000 + (fractpart*1000)/0x100000000L;
            return milliseconds;
        }

        // Compute the 8-byte array, given the date
        private void setDate(byte offset, DateTime date)
        {
            var startOfCentury = new DateTime(1900, 1, 1, 0, 0, 0); // January 1, 1900 12:00 AM

            var milliseconds = (ulong) (date - startOfCentury).TotalMilliseconds;
            ulong intpart = milliseconds/1000;
            ulong fractpart = ((milliseconds%1000)*0x100000000L)/1000;

            ulong temp = intpart;
            for (int i = 3; i >= 0; i--)
            {
                _ntpData[offset + i] = (byte) (temp%256);
                temp = temp/256;
            }

            temp = fractpart;
            for (int i = 7; i >= 4; i--)
            {
                _ntpData[offset + i] = (byte) (temp%256);
                temp = temp/256;
            }
        }

        // Initialize the NTPClient data
        private void initialize()
        {
            // Set version number to 4 and Mode to 3 (client)
            _ntpData[0] = 0x1B;
            // Initialize all other fields with 0
            for (int i = 1; i < 48; i++)
            {
                _ntpData[i] = 0;
            }
            // Initialize the transmit timestamp
            transmitTimestamp = DateTime.Now;
        }

        // Connect to the time server and update system time
        public void connect(bool updateSystemTime)
        {
            try
            {
                // Resolve server address
#pragma warning disable 612,618
                IPHostEntry hostadd = Dns.Resolve(_timeServer);
#pragma warning restore 612,618
                var ePhost = new IPEndPoint(hostadd.AddressList[0], 123);

                //Connect the time server
                var timeSocket = new UdpClient();
                timeSocket.Connect(ePhost);

                // Initialize data structure
                initialize();
                timeSocket.Send(_ntpData, _ntpData.Length);
                _ntpData = timeSocket.Receive(ref ePhost);
                if (!isResponseValid())
                {
                    throw new Exception("Invalid response from " + _timeServer);
                }
                ReceptionTimestamp = DateTime.Now;
            }
            catch (SocketException e)
            {
                throw new Exception(e.Message);
            }

            // Update system time
            if (updateSystemTime)
            {
                setTime();
            }
        }

        // Check if the response from server is valid
        public bool isResponseValid()
        {
            if (_ntpData.Length < NTPDataLength || mode != Mode.Server)
            {
                return false;
            }
            return true;
        }

        // Converts the object to string
        public override string ToString()
        {
            string str = "Leap Indicator: ";
            switch (leapIndicator)
            {
                case LeapIndicator.NoWarning:
                    str += "No warning";
                    break;
                case LeapIndicator.LastMinute61:
                    str += "Last minute has 61 seconds";
                    break;
                case LeapIndicator.LastMinute59:
                    str += "Last minute has 59 seconds";
                    break;
                case LeapIndicator.Alarm:
                    str += "Alarm Condition (clock not synchronized)";
                    break;
            }
            str += "\r\nVersion number: " + versionNumber.ToString(CultureInfo.InvariantCulture) + "\r\n";
            str += "Mode: ";
            switch (mode)
            {
                case Mode.Unknown:
                    str += "Unknown";
                    break;
                case Mode.SymmetricActive:
                    str += "Symmetric Active";
                    break;
                case Mode.SymmetricPassive:
                    str += "Symmetric Pasive";
                    break;
                case Mode.Client:
                    str += "Client";
                    break;
                case Mode.Server:
                    str += "Server";
                    break;
                case Mode.Broadcast:
                    str += "Broadcast";
                    break;
            }
            str += "\r\nStratum: ";
            switch (stratum)
            {
                case Stratum.Unspecified:
                case Stratum.Reserved:
                    str += "Unspecified";
                    break;
                case Stratum.PrimaryReference:
                    str += "Primary Reference";
                    break;
                case Stratum.SecondaryReference:
                    str += "Secondary Reference";
                    break;
            }
            str += "\r\nLocal time: " + transmitTimestamp.ToString(CultureInfo.InvariantCulture);
            str += "\r\nPrecision: " + precision.ToString(CultureInfo.InvariantCulture) + " ms";
            str += "\r\nPoll Interval: " + pollInterval.ToString(CultureInfo.InvariantCulture) + " s";
            str += "\r\nReference ID: " + referenceId;
            str += "\r\nRoot Dispersion: " + rootDispersion.ToString(CultureInfo.InvariantCulture) + " ms";
            str += "\r\nRound Trip Delay: " + roundTripDelay.ToString(CultureInfo.InvariantCulture) + " ms";
            str += "\r\nLocal Clock Offset: " + localClockOffset.ToString(CultureInfo.InvariantCulture) + " ms";
            str += "\r\n";

            return str;
        }

        // SYSTEMTIME structure used by SetSystemTime

        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref Systemtime time);


        // Set system time according to transmit timestamp
        private void setTime()
        {
            Systemtime st;

            DateTime trts = transmitTimestamp;
            st.year = (short) trts.Year;
            st.month = (short) trts.Month;
            st.dayOfWeek = (short) trts.DayOfWeek;
            st.day = (short) trts.Day;
            st.hour = (short) trts.Hour;
            st.minute = (short) trts.Minute;
            st.second = (short) trts.Second;
            st.milliseconds = (short) trts.Millisecond;

            SetLocalTime(ref st);
            //logger.Info(TransmitTimestamp);
        }

        #region Nested type: Systemtime

        [StructLayout(LayoutKind.Sequential)]
        private struct Systemtime
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        #endregion

        // The URL of the time server we're connecting to
    }
}