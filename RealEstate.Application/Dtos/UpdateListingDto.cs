﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Dtos
{
    public class UpdateListingDto: CreateListingDto
    {
        [Required] public Guid Id { get; set; }

    }
}
