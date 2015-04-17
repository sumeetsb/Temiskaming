﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(appointmentValdation))]
    public partial class appointment
    { }

    [Bind(Exclude="id")]
    public class appointmentValdation           // Validation Model for appointment model
    {
        [Required]
        public int doctor_id;

        [DisplayName("Appointment Date")]
        [Required(ErrorMessage = "Please choose a date")]
        public System.DateTime booking_date;                   

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string email;

        [DisplayName("Phone")]
        [Required(ErrorMessage="Enter a phone number")]
        [RegularExpression("^(\\([0-9]{3}\\) |[0-9]{3}-)[0-9]{3}-[0-9]{4}$",ErrorMessage="Enter a valid phone number")]
        public string phone;
    }

}