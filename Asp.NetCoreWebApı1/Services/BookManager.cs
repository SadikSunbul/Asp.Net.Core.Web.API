﻿using AutoMapper;
using Entities.DTO_DataTransferObject_;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Ripositories.Contracts;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogerService _logerService;
        private readonly IMapper _mapper;
        private readonly IBookLinks _bookLinks;
        public BookManager(IRepositoryManager manager, ILogerService logerService, IMapper mapper, IBookLinks bookLinks)
        {
            _manager = manager;
            _logerService = logerService;
            _mapper = mapper;
            this._bookLinks = bookLinks;
        }



        public async Task<BookDto> CreateOneBookAsync(BookDto book)
        {
            var entity = _mapper.Map<Book>(book);
            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool tracking = true)
        {
            //check entıtıy
            var entıty = await GetOneBookAndChackExits(id, tracking);
            _manager.Book.DeleteOneBook(entıty);
            await _manager.SaveAsync();
        }

        public async Task<(LinkRespons linkRespons, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool tracking = true)
        {
            if (!linkParameters.BookParameters.ValidPriceRange)
            {
                throw new PriceOutıofRangeBadRequestException();
            }
            var bookswithMetaData = await _manager.Book.GetAllBooksAsync(linkParameters.BookParameters);

            var bookdDto = _mapper.Map<IEnumerable<BookDto>>(bookswithMetaData);
            //IEnumerable<BookDto> TÜRÜNE döndür book u
            //boyle bı gecıs varmı yokmu confıge gıdıp eklenmelidir
            var links = _bookLinks.TryGenerateLinks(bookdDto, linkParameters.BookParameters.Fields, linkParameters.HttpContext);
            return (linkRespons: links, metaData: bookswithMetaData.MetaData);
        }

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
           return await _manager.Book.GetAllBooksAsync(trackChanges);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool tracking = true)
        {
            var book = await GetOneBookAndChackExits(id, tracking);
            return _mapper.Map<BookDto>(await _manager.Book.GetOneBooksByIdAsync(id, tracking));
        }



        public async Task UpdateOneBookAsync(int id, BookDTOForUpdate bookDto)
        {
            var entity = await _manager.Book.GetOneBooksByIdAsync(id);
            if (entity is null)
            {
                throw new Exception($"book whit id: {id} could not found.");
            }

            //check parms
            if (bookDto is null)
            {
                throw new ArgumentException(nameof(bookDto));

            }
            //Mappıng
            //entity.Title = book.Title;
            //entity.Price = book.Price;

            entity = _mapper.Map<Book>(bookDto);
            //Burada olan olay BookDto yu ---> Book a döndürdü


            _manager.Book.Update(entity);
            await _manager.SaveAsync();

        }

        private async Task<Book> GetOneBookAndChackExits(int id, bool tracking)
        {
            var entıty = await _manager.Book.GetOneBooksByIdAsync(id, tracking);
            if (entıty is null)
            {
                string message = $"The book with id:{id} colf not found";
                _logerService.LogInfo(message);
                throw new Exception(message);
            }
            return entıty;
        }
    }
}
