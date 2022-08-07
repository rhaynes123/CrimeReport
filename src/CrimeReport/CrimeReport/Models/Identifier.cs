using System;
using shortid;
using shortid.Configuration;
namespace CrimeReport.Models
{
    public class Identifier
    {
        private readonly GenerationOptions options;
        public Identifier()
        {
            options = new(useSpecialCharacters: false, length: 10);
        }
        public Identifier(GenerationOptions customOptions)
        {
            options = customOptions;
        }
        public string New()
        {
            return ShortId.Generate(options);
        }
    }
}

