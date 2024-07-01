namespace AkuzelUI.Dtos.ResponseDtos
{
    public class ListeVerisResponse
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public Guid? UstId { get; set; }
        public int Derinlik { get; set; }
        public string Deger { get; set; }
        public Guid? EkId { get; set; }
        public string? EkDeger { get; set; }
        public string? Aciklama { get; set; }
        public Guid DuzenleyenId { get; set; }
    }
}
