using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day2PasswordPhilosophy
{
    public static class PasswordPhilosophy
    {
        public static int CountValidPasswords<T>() where T : IPasswordInfoBase, new()
        {
            var passwordInfo = File.ReadAllLines("Day2PasswordPhilosophy/passwordData.txt")
                .Select(pd => new T().ParseFromData(pd));

            return passwordInfo.Count(pi => pi.IsValid());
        }
    }

    public class PasswordInfoWithRange : IPasswordInfoBase
    {
        private int MinOccurrences { get; set; }
        private int MaxOccurrences { get; set; }
        private string Restricted { get; set; }
        private string Password { get; set; }

        public bool IsValid()
        {
            var occurrences = Regex.Matches(Password, Restricted).Count;

            return MinOccurrences <= occurrences && occurrences <= MaxOccurrences;
        }

        public IPasswordInfoBase ParseFromData(string passwordData)
        {
            var rangeSeparatorIndex = passwordData.IndexOf('-');

            return new PasswordInfoWithRange
            {
                Password = passwordData.Substring(passwordData.IndexOf(':') + 1).Trim(),
                Restricted = passwordData.Substring(passwordData.IndexOf(':') - 1, 1),

                MinOccurrences = int.Parse(passwordData.Substring(0, rangeSeparatorIndex).Trim()),
                MaxOccurrences =
                    int.Parse(passwordData
                        .Substring(rangeSeparatorIndex + 1, passwordData.IndexOf(' ') - (rangeSeparatorIndex + 1))
                        .Trim()),
            };
        }
    }

    public class PasswordInfoWithPositions : IPasswordInfoBase
    {
        private int[] AllowedPositions { get; set; }
        private char Restricted { get; set; }

        private string Password { get; set; }

        public bool IsValid()
        {
            return AllowedPositions
                .Select(i => Password[i - 1])
                .Count(x => x == Restricted) == 1;
        }

        public IPasswordInfoBase ParseFromData(string passwordData)
        {
            return new PasswordInfoWithPositions
            {
                Password = passwordData.Substring(passwordData.IndexOf(':') + 1).Trim(),
                Restricted = passwordData[passwordData.IndexOf(':') - 1],

                AllowedPositions = passwordData
                    .Substring(0, passwordData.IndexOf(' '))
                    .Split('-')
                    .Select(int.Parse)
                    .ToArray()
            };
        }
    }

    public interface IPasswordInfoBase
    {
        public bool IsValid();

        public IPasswordInfoBase ParseFromData(string passwordData);
    }
}