using System;

namespace Test.Web.Models
{
    /// <summary>
    /// 用户信息基类
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public abstract class UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
    }

    public class User : UserBase<Guid>
    {
        public Guid Id { get; set; }

        public int Num { get; set; }
    }
}