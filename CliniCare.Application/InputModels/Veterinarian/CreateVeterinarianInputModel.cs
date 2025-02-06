﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.InputModels.Veterinarian
{
    public class CreateVeterinarianInputModel
    {
        public string CRMV { get; set; }
        public string Specialty { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
