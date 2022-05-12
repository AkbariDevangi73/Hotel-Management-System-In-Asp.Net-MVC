using HotelManagement.Models;
using HotelManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class BookingController : Controller
    {
        private HotelDBEntities objHotelDBEntities;
        public BookingController()
        {
            objHotelDBEntities = new HotelDBEntities();
        }
        // GET: Booking
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms =    (from objRoom in objHotelDBEntities.Rooms
                                               where objRoom.BookingStatusId == 2
                                               select new SelectListItem()
                                               {
                                                   Text=objRoom.RoomNumber,
                                                   Value=objRoom.RoomId.ToString()

                                               }).ToList();
            objBookingViewModel.BookingFrom = DateTime.Now;
            objBookingViewModel.BookingTo = DateTime.Now.AddDays(1);
            return View(objBookingViewModel);
        }

        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel) 
        {
            int numberOfDays = Convert.ToInt32((objBookingViewModel.BookingTo - objBookingViewModel.BookingFrom).TotalDays);
            Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId == objBookingViewModel.AssignRoomId);
            decimal RoomPrice = objRoom.RoomPrice;
            decimal TotalAmount = RoomPrice * numberOfDays;

            RoomBooking roomBooking = new RoomBooking()
            {
                BookingFrom=objBookingViewModel.BookingFrom,
                BookingTo=objBookingViewModel.BookingTo,
                AssignRoomId=objBookingViewModel.AssignRoomId,
                CustomerAddress=objBookingViewModel.CustomerAddress,
                CustomerName=objBookingViewModel.CustomerName,
                CustomerPhone=objBookingViewModel.CustomerPhone, 
                NoOfMembers=objBookingViewModel.NoOfMembers,
                TotalAmount=TotalAmount
            };
            objHotelDBEntities.RoomBookings.Add(roomBooking);
            objHotelDBEntities.SaveChanges();

            objRoom.BookingStatusId = 3;
            objHotelDBEntities.SaveChanges();
            return Json(new { message = "Hotel Booking is Successfully Created..",success=true}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetAllBookingHistory()
        {
            List<RoomBookingViewModel> listOfBookingViewModels = new List<RoomBookingViewModel>();
            listOfBookingViewModels = (from objHotelBooking in objHotelDBEntities.RoomBookings
                                       join objRoom in objHotelDBEntities.Rooms on objHotelBooking.AssignRoomId
                                       equals objRoom.RoomId
                                       select new RoomBookingViewModel()
                                       {
                                           BookingFrom=objHotelBooking.BookingFrom,
                                           BookingTo=objHotelBooking.BookingTo,
                                           CustomerPhone=objHotelBooking.CustomerPhone,
                                           CustomerName=objHotelBooking.CustomerName,
                                           TotalAmount=objHotelBooking.TotalAmount,
                                           CustomerAddress=objHotelBooking.CustomerAddress,
                                           NoOfMembers=objHotelBooking.NoOfMembers,
                                           BookingId=objHotelBooking.BookingId,
                                           RoomNumber=objRoom.RoomNumber,
                                           RoomPrice=objRoom.RoomPrice,
                                           NumberOfDays=System.Data.Entity.DbFunctions.DiffDays(objHotelBooking.BookingFrom,objHotelBooking.BookingTo).Value
                                       }).ToList();
            return PartialView("_BookingHistoryPartialView", listOfBookingViewModels);
        }
    }
}