using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiPractice.Contracts;
using webApiPractice.Data;
using webApiPractice.Interface;

namespace webApiPractice.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commetModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commetModel == null) return null;
            _context.Comments.Remove(commetModel);
            _context.SaveChanges();
            return commetModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var model = await _context.Comments.ToListAsync();
            return model;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment= await _context.Comments.FindAsync(id);
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
           var exist = await _context.Comments.FindAsync(id);
            if (exist == null)
            {
                return null;
            }
            exist.Title = commentModel.Title;
            exist.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return exist;
        }
    }
}
