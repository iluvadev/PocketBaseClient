// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

namespace PocketBaseClient.CodeGenerator
{
    class Program
    {
        public static string Banner = @"
  _____           _        _   ____                  _____ _ _            _   
 |  __ \         | |      | | |  _ \                / ____| (_)          | |  
 | |__) |__   ___| | _____| |_| |_) | __ _ ___  ___| |    | |_  ___ _ __ | |_ 
 |  ___/ _ \ / __| |/ / _ \ __|  _ < / _` / __|/ _ \ |    | | |/ _ \ '_ \| __|
 | |  | (_) | (__|   <  __/ |_| |_) | (_| \__ \  __/ |____| | |  __/ | | | |_ 
 |_|   \___/ \___|_|\_\___|\__|____/ \__,_|___/\___|\_____|_|_|\___|_| |_|\__|
   _____          _       _____                           _                   
  / ____|        | |     / ____|                         | |                  
 | |     ___   __| | ___| |  __  ___ _ __   ___ _ __ __ _| |_ ___  _ __       
 | |    / _ \ / _` |/ _ \ | |_ |/ _ \ '_ \ / _ \ '__/ _` | __/ _ \| '__|      
 | |___| (_) | (_| |  __/ |__| |  __/ | | |  __/ | | (_| | || (_) | |         
  \_____\___/ \__,_|\___|\_____|\___|_| |_|\___|_|  \__,_|\__\___/|_|         
";


        public static string Welcome = @$"

              Welcome to PocketBaseClient CodeGenerator
 An application to generate client side code in c# for your PocketBase application
";

        static int Main(string[] args)
        {
            Interactive.Process.Start();
            return 0;
        }
    }
}