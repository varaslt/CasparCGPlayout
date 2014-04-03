/*
 * NTPClient
 * Copyright (C)2001 Valer BOCAN <[[Email Removed]]>
 * Last modified: June 29, 2001
 * All Rights Reserved
 * 
 * This code is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY, without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * To fully understand the concepts used herein, I strongly
 * recommend that you read the RFC 2030.
 * 
 * NOTE: This example is intended to be compiled with Visual Studio .NET Beta 2
 */

namespace CasparCGPlayout.Utils
{ // Leap indicator field values
    public enum LeapIndicator
    {
        NoWarning,            // 0 - No warning
        LastMinute61,   // 1 - Last minute has 61 seconds
        LastMinute59,   // 2 - Last minute has 59 seconds
        Alarm         // 3 - Alarm condition (clock not synchronized)
    }

    //Mode field values
    public enum Mode
    {
        SymmetricActive,        // 1 - Symmetric active
        SymmetricPassive,       // 2 - Symmetric pasive
        Client,    // 3 - Client
        Server,    // 4 - Server
        Broadcast,                  // 5 - Broadcast
        Unknown    // 0, 6, 7 - Reserved
    }

    // Stratum field values
    public enum Stratum
    {
        Unspecified,            // 0 - unspecified or unavailable
        PrimaryReference,              // 1 - primary reference (e.g. radio-clock)
        SecondaryReference,          // 2-15 - secondary reference (via NTP or SNTP)
        Reserved                                // 16-255 - reserved
    }
}