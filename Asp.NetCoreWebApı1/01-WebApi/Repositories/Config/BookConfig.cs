﻿using _01_WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_WebApi.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "kitap 1", Price = 23 },
                new Book { Id = 2, Title = "kitap 2", Price = 223 },
                new Book { Id = 3, Title = "kitap 3", Price = 233 }
                );
        }
    }
}
