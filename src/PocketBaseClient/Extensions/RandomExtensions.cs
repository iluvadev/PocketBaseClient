// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Text;

namespace PocketBaseClient
{
    public static class RandomExtensions
    {
        //const string defaultRandomAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const string defaultRandomAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789";

        // PseudorandomString generates a pseudorandom string with the specified length.
        //
        // The generated string matches [A-Za-z0-9]+ and it's transparent to URL-encoding.
        //
        // For a cryptographically random string (but a little bit slower) use RandomString instead.
        public static string PseudorandomString(this Random rnd, int length)
        {
            return PseudorandomStringWithAlphabet(rnd, length, defaultRandomAlphabet);
        }

        // PseudorandomStringWithAlphabet generates a pseudorandom string
        // with the specified length and characters set.
        //
        // For a cryptographically random (but a little bit slower) use RandomStringWithAlphabet instead.
        public static string PseudorandomStringWithAlphabet(this Random rnd, int length, string alphabet)
        {
            var sb = new StringBuilder();
            var max = alphabet.Length;

            for (int i = 0; i < length; i++)
                sb.Append(alphabet[rnd.Next(max)]);
            return sb.ToString();
        }
    }
}
