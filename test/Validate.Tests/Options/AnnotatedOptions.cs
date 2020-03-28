﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Validate.Tests.Options
{
    public class AnnotatedOptions
    {
        [Required]
        public string Required { get; set; }

        [StringLength(5, ErrorMessage = "Too long.")]
        public string StringLength { get; set; }

        [Range(-5, 5, ErrorMessage = "Out of range.")]
        public int IntRange { get; set; }

        public string Custom { get; internal set; }
    }
}