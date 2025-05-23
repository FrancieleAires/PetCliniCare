﻿using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.VeterinaryCare.GetAllVetCare
{
    public class GetAllVetCaresQuery : IRequest<Result<List<VetCareViewModel>>>
    {
        public GetAllVetCaresQuery()
        {
        }
    }
}
