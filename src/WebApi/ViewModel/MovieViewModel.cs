﻿namespace WebApi.ViewModel
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int Favorite_count { get; set; }

        public int Item_count { get; set; }
        public string? Iso_639_1 { get; set; }
        public string? list_type { get; set; }
        public string? Name { get; set; }

        public int PageId { get; set; }
    }
}
