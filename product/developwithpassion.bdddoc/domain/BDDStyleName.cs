using System;

namespace developwithpassion.bdddoc.domain
{
    public class BDDStyleName : IEquatable<BDDStyleName>
    {
        public string name { get; private set; }

        public BDDStyleName(string raw_name)
        {
            name = format_using_bdd_naming_styles(raw_name);
        }

        private string format_using_bdd_naming_styles(string item)
        {
            var result = replace_spaces_surrounding_an_s_with_an_apostrophe(item);
            result = replace_spaces_with_underscores(result);
            return result;
        }

        private string replace_spaces_with_underscores(string item)
        {
            return item.Replace('_', ' ');
        }

        private string replace_spaces_surrounding_an_s_with_an_apostrophe(string item)
        {
            return item.Replace("_s_", "'s ");
        }

        public static implicit operator string(BDDStyleName name)
        {
            return name.ToString();
        }

        public override string ToString()
        {
            return name;
        }

        public bool Equals(BDDStyleName other)
        {
            if (other == null) return false;

            return ReferenceEquals(this, other) ? true : string.Compare(name, other.name, false) == 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BDDStyleName);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}