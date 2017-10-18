using System;

namespace ISX
{
    // Wraps around a string to allow for faster case-insensitive
    // string comparisons while preserving original casing.
    public struct InternedString : IEquatable<InternedString>, IComparable<InternedString>
    {
        private readonly string m_StringOriginalCase;
        private readonly string m_StringLowerCase;

        public InternedString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                m_StringOriginalCase = null;
                m_StringLowerCase = null;
            }
            else
            {
                ////TODO: I think instead of string.Intern() this should use a custom weak-referenced intern table
                m_StringOriginalCase = string.Intern(text);
                m_StringLowerCase = string.Intern(text.ToLower());
            }
        }

        public bool IsEmpty()
        {
            return m_StringLowerCase == null;
        }

        public string ToLower()
        {
            return m_StringLowerCase;
        }

        public bool Equals(InternedString other)
        {
            return ReferenceEquals(m_StringLowerCase, other.m_StringLowerCase);
        }

        public int CompareTo(InternedString other)
        {
            return string.Compare(m_StringLowerCase, other.m_StringLowerCase);
        }

        public override int GetHashCode()
        {
            if (m_StringLowerCase == null)
                return 0;
            return m_StringLowerCase.GetHashCode();
        }

        public override string ToString()
        {
            return m_StringOriginalCase ?? string.Empty;
        }

        public static bool operator==(InternedString a, InternedString b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(InternedString a, InternedString b)
        {
            return !a.Equals(b);
        }

        public static implicit operator string(InternedString str)
        {
            return str.m_StringOriginalCase;
        }
    }
}