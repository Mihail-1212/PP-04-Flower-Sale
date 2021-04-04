﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowersSale.Utils
{
    public enum Gender
    {
        Man,
        Woman
    }

    public enum Roll
    {
        Admin,
        User
    }

    public static class Constants
    {
        public const String LOGIN_KEY = "LOGIN_KEY";
        public const String PASSWORD_KEY = "PASSWORD_KEY";
        public const String LOGIN_REGEX = @"^[\w]{2}\@[\w]{2}\.[\w]{2}$";
        public const String PASSWORD_REGEX = @"^(?=.*[A-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!?+=_-])\S{8,255}$";
        public const String ALPHABET_REGEX = @"^[a-zA-Zа-яА-ЯёЁ]{1,255}$";
        public const String PHONE_REGEX = @"^8\d{10}|\+7\d{10}$";
    }
}