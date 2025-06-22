using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dbfEvent.Web.Models.ViewModel
{
	public class TicketPurchaseVM
	{

        public int EventId { get; set; }
        public string EventType { get; set; }

        // Dropdown listeleri View tarafında kullanmak için
        public List<SelectListItem> EventTypes { get; set; }
        public List<SelectListItem> Events { get; set; }


		[Required(ErrorMessage = "Ad Soyad Zorunludur.")]
		public string? ParticipantName { get; set; }

        [Range(1, 120, ErrorMessage = "Geçerli bir yaş giriniz.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Telefon Numarası Zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Bilet tipi seçiniz.")]
        public string TicketType { get; set; }

        public List<SelectListItem> TicketTypes { get; set; }

        public string TicketStatus { get; set; }  // Bilet durumu gösterim için


    }
}

