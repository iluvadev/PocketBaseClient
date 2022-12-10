using Sharprompt;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PocketBaseClient.CodeGenerator
{
    internal static class Interactive
    {
        enum MainActions
        {
            [Display(Name = "Generate Code")]
            GenerateCode,
            [Display(Name = "Download PocketBase Schema")]
            DownloadSchema,
        }

        public static void Start()
        {
            //Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Welcome to PocketBaseClient Code Generator: An application to generate client side code of your PocketBase application");
            Console.WriteLine();

            var mainAction = Prompt.Select<MainActions>("Select action to do");
            switch (mainAction)
            {
                case MainActions.GenerateCode: GenerateCode(); break;
                case MainActions.DownloadSchema: DownloadSchema(); break;
            }
        }

        enum OriginCode
        {
            [Display(Name = "Directly from PocketBase server")]
            FromPocketBase,
            [Display(Name = "From downloaded schema")]
            FromSchema,
        }
        private static void GenerateCode()
        {
            var originCode = Prompt.Select<OriginCode>("Select where to find the PocketBase schema");

            string? schemaFile = null;
            switch (originCode)
            {
                case OriginCode.FromPocketBase:
                    schemaFile = DownloadSchema(true);
                    break;

                case OriginCode.FromSchema:
                    schemaFile = Prompt.Input<string>("Enter fileName with the downloaded Schema file",
                                                      validators: ValidationResultsForLoadSchemaFileName());
                    break;
            }
            if (schemaFile == null)
            {
                Console.WriteLine("There was an error: schemaFile is null!");
                Console.WriteLine("Aborting :(");
                return;
            }

            var genNamespace = Prompt.Input<string>("Enter namespace for generated code",
                                                    validators: ValidationResultsForGeneratedNamespace()).Replace(" ","_");

            bool ok = false;
            string? outputPath = null;
            while (!ok)
            {
                outputPath = Prompt.Input<string>("Enter output path for the generated code",
                                                  validators: ValidationResultsForGenerateFolder());
                if (Directory.Exists(outputPath))
                    ok = Prompt.Confirm("Path already exists, do you want to overwrite generated files?", false);
                else
                    ok = true;
            }
            CodeGenerator.GenerateCode(schemaFile, outputPath, genNamespace);
        }
        private static string DownloadSchema(bool tempFile = false)
        {
            var pbConnection = Prompt.Bind<PocketBaseConnection>();
            string fileName = "";
            if (tempFile)
                fileName = Path.GetTempFileName();
            else
            {
                bool ok = false;
                while (!ok)
                {
                    fileName = Prompt.Input<string>("Enter fileName for the downloaded Schema file",
                                                    validators: ValidationResultsForSaveSchemaFileName());
                    if (File.Exists(fileName))
                        ok = Prompt.Confirm("File already exists, do you want to overwrite it?", false);
                    else
                        ok = true;
                }
            }
            SchemaDownloader.DownloadSchema(pbConnection.Uri, pbConnection.Email, pbConnection.Password, new FileInfo(fileName)).Wait();

            return fileName;
        }

        private static List<Func<object, ValidationResult>> ValidationResultsForSaveSchemaFileName()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
                value =>
                {
                    if (value is string str && !Directory.Exists(Path.GetDirectoryName(str)))
                        return new ValidationResult("Path do not exists");
                    return ValidationResult.Success;
                },
            };
        }
        private static List<Func<object, ValidationResult>> ValidationResultsForLoadSchemaFileName()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
                value =>
                {
                    if (value is string str && !File.Exists(str))
                        return new ValidationResult("File do not exists");
                    return ValidationResult.Success;
                },
            };
        }
        private static List<Func<object, ValidationResult>> ValidationResultsForGenerateFolder()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
            };
        }
        private static List<Func<object, ValidationResult>> ValidationResultsForGeneratedNamespace()
        {
            return new List<Func<object, ValidationResult>>()
            {
                Validators.Required(),
                Validators.MinLength(3),
            };
        }


        private class PocketBaseConnection
        {
            [Display(Name = "Type PocketBase server Url")]
            [DataType(DataType.Url)]
            [Required]
            public string Url { get; set; }
            public Uri Uri => new Uri(Url);

            [Display(Name = "Type Admin email")]
            [DataType(DataType.EmailAddress)]
            [Required]
            [MinLength(5)]
            public string Email { get; set; }


            [Display(Name = "Type the Admin password")]
            [DataType(DataType.Password)]
            [Required]
            public string Password { get; set; }
        }
    }
}
