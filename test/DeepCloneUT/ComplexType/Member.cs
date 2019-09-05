using System;

namespace DeepCloneUT
{
    public class Member
    {
        private long? _id;

        public long Id
        {
            get
            {
                if (_id == null)
                {
                    _id = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                }
                return _id.Value;
            }
            set
            {
                _id = value;
            }
        }

        public MemberType MemberType;

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName => $"{LastName}{(string.IsNullOrEmpty(MiddleName) ? string.Empty : $"-{MiddleName}")}-{FirstName}";

        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 中间名
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>
        public decimal AnnualIncome { get; set; }

        /// <summary>
        /// 老师
        /// </summary>
        public Member Teacher { get; set; }

        public override bool Equals(object obj)
        {
            var toCompareWith = obj as Member;
            if (toCompareWith == null)
                return false;
            return
                    this.Id == toCompareWith.Id &&
                    this.MemberType == toCompareWith.MemberType &&
                    this.FullName == toCompareWith.FullName &&
                    this.FirstName == toCompareWith.FirstName &&
                    this.MiddleName == toCompareWith.MiddleName &&
                    this.LastName == toCompareWith.LastName &&
                    this.Age == toCompareWith.Age &&
                    this.Birthday == toCompareWith.Birthday &&
                    this.AnnualIncome == toCompareWith.AnnualIncome &&
                    (this.Teacher == null && toCompareWith.Teacher == null ? true : this.Teacher.Equals(toCompareWith.Teacher));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
