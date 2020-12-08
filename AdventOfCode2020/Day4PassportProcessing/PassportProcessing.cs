using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day4PassportProcessing
{
    public class PassportProcessing
    {
        private static IEnumerable<Dictionary<Fields, string>> ReadPassportData()
        {
            string file = File.ReadAllText("Day4PassportProcessing/passports.txt");

            return file
                .Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new[] {" ", Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                    .ToDictionary(
                        field => Enum.Parse<Fields>(field.Split(':')[0], true),
                        field => field.Split(':')[1]));
        }

        public int CountPassportsWithAllRequiredFields()
        {
            IEnumerable<Fields> requiredFields = ((Fields[]) Enum.GetValues(typeof(Fields)))
                .Except(new[] {Fields.Cid});

            return ReadPassportData()
                .Count(p =>
                {
                    List<Fields> presentFields = p.Keys
                        .OrderBy(x => x)
                        .ToList();

                    return !requiredFields.Except(presentFields).Any();
                });
        }

        public int CountValidPassports()
        {
            return ReadPassportData()
                .Select(Passport.ParseFromPassportData)
                .Count(Passport.AllFieldsValid);
        }
    }
    
     internal enum Unit
    {
        Centimetres,
        Inches
    }

    internal struct HeightMeasurement
    {
        public Unit Unit { get; set; }
        public int Value { get; set; }
    }

    internal enum EyeColour
    {
        Amb,
        Blu,
        Brn,
        Gry,
        Grn,
        Hzl,
        Oth
    }

    internal enum Fields
    {
        Byr,
        Iyr,
        Eyr,
        Hgt,
        Hcl,
        Ecl,
        Pid,
        Cid
    };

    internal class Passport
    {
        public int BirthYear { get; set; }

        public int IssueYear { get; set; }

        public int ExpirationYear { get; set; }

        public HeightMeasurement Height { get; set; }

        public string HairColour { get; set; }

        public EyeColour? EyeColour { get; set; }

        public string PassportId { get; set; }

        public int CountryId { get; set; }

        public static Passport ParseFromPassportData(Dictionary<Fields, string> passportData)
        {
            var passport = new Passport();

            foreach (Fields field in passportData.Keys)
            {
                string value = passportData[field];

                switch (field)
                {
                    case Fields.Byr:
                        passport.BirthYear = int.Parse(value);
                        continue;
                    case Fields.Iyr:
                        passport.IssueYear = int.Parse(value);
                        continue;
                    case Fields.Eyr:
                        passport.ExpirationYear = int.Parse(value);
                        continue;
                    case Fields.Hgt:
                        passport.Height = new HeightMeasurement
                        {
                            Unit = value.Contains("cm") ? Unit.Centimetres : Unit.Inches,
                            Value = int.Parse(new string(value.TakeWhile(char.IsDigit).ToArray()))
                        };
                        continue;
                    case Fields.Hcl:
                        passport.HairColour = value ?? string.Empty;
                        continue;
                    case Fields.Ecl:
                        passport.EyeColour = Enum.TryParse(value, true, out EyeColour parsed) ? parsed : (EyeColour?) null;
                        continue;
                    case Fields.Pid:
                        passport.PassportId = value ?? string.Empty;
                        continue;
                    case Fields.Cid:
                        passport.CountryId = int.Parse(value);
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return passport;
        }

        public static bool AllFieldsValid(Passport passport)
        {
            return new List<Func<Passport, bool>>
            {
                p => 1920 <= p.BirthYear && p.BirthYear <= 2002,
                p => 2010 <= p.IssueYear && p.IssueYear <= 2020,
                p => 2020 <= p.ExpirationYear && p.ExpirationYear <= 2030,
                p => p.Height.Unit == Unit.Centimetres
                    ? 150 <= p.Height.Value && p.Height.Value <= 193
                    : 59 <= p.Height.Value && p.Height.Value <= 76,
                p => Regex.Match(p.HairColour ?? string.Empty, "#[a-f0-9]{6}( |$)").Success,
                p => p.PassportId?.Length == 9,
                p => p.EyeColour != null,
            }.All(validate => validate(passport));
        }
    }
}