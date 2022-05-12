using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name ="Customer Name")]
        [Required(ErrorMessage ="Customer Name is Required.")]
        public string CustomerName { get; set; }

        [Display(Name ="Customer Address")]
        [Required(ErrorMessage = "Customer Address is Required.")]
        public string CustomerAddress { get; set; }

        [Display(Name ="Customer PhoneNumber")]
        [Required(ErrorMessage = "Customer PhoneNumber is Required.")]
        public string CustomerPhone { get; set; }

        [Display(Name ="Booking Form")]
        //[DataType(DataType.Date)]
        [Required(ErrorMessage = "Booking From is Required.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingFrom { get; set; }

        [Display(Name ="Booking To")]
        //[DataType(DataType.Date)]
        [Required(ErrorMessage = "Booking To is Required.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingTo { get; set; }

        [Display(Name ="Assign Room")]
        public int AssignRoomId { get; set; }

        [Display(Name ="Number of Members")]
        [Required(ErrorMessage = "Number of Member is Required.")]
        public int NoOfMembers { get; set; }
        public IEnumerable<SelectListItem> ListOfRooms { get; set; }

    }
}