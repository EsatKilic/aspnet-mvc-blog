﻿using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Linq;
using System;
using App.Business.Services.Abstract;
using App.Data.Entity;
using App.Data;

namespace App.Business.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _db;

        public PostService(AppDbContext db)
        {
            // Post koleksiyonunu başlatma veya veritabanından veri çekme işlemleri burada yapılabilir
            _db = db;
        }

        public List<Post> GetAll()
        {
            return _db.Posts.Include(p => p.Categories).Include(p => p.User).ToList();

        }

        public Post GetById(int id)
        {
            return _db.Posts
                .Include(p => p.Categories)
                .Include(p => p.User)
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public void Insert(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public void Update(int id, Post post)
        {
            var oldPost = _db.Posts.FirstOrDefault(p => p.Id == post.Id);
            if (oldPost != null)
            {
                _db.Entry(oldPost).CurrentValues.SetValues(post);
                _db.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var post = _db.Posts.SingleOrDefault(p => p.Id == id);
            if (post != null)
            {
                _db.Posts.Remove(post);
                _db.SaveChanges();
            }
        }


    }
}