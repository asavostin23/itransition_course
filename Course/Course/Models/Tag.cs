﻿namespace Course.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Item> Items { get; set; } = new();
        public Tag(string Name) => this.Name = Name;
    }
}
