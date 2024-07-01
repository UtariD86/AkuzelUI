using Microsoft.AspNetCore.Mvc.Rendering;

namespace AkuzelUI.Areas.Admin.Models
{
    public class ListeVeriEditViewModel
    {
        public SelectList? TypeList { get; set; }

        public Guid? Id { get; set; }

        public int TypeId { get; set; }

        public string? Type { get; set; }

        public Guid? UstId { get; set; }

        public int Derinlik { get; set; }

        public string Deger { get; set; }

        public Guid? EkId { get; set; }

        public string? EkDeger { get; set; }

        public string? Aciklama { get; set; }

    }
}
