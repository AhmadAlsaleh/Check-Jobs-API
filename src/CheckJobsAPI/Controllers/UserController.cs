using CheckJobsAPI.Data;
using CheckJobsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace CheckJobsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        ApplicationDbContext _context { get; set; }

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        [HttpPost]
        public JsonResult RegisterUser([FromBody] User user)
        {
            if (_context.Users.Where(x => x.Email == user.Email).SingleOrDefault() != null)
            {
                return Json(BadRequest("Already existed"));
            }

            try
            {
                user.ID = Guid.NewGuid();
                _context.Users.Add(user);
                _context.SaveChanges();
                return Json(new { user.ID });
            } catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpPost]
        public JsonResult LoginUser([FromBody] User user)
        {
            if (_context.Users.Where(x => x.Email == user.Email).SingleOrDefault() != null)
            {
                User userSelect = _context.Users
                    .Where(x => x.Email == user.Email
                            && x.Password == user.Password)
                    .SingleOrDefault();
                if (userSelect != null)
                {
                    return Json(userSelect);
                }
                else
                {
                    return Json(BadRequest("Incorrect Password"));
                }
            }
            else
            {
                return Json(BadRequest("Your email hasn\'t registered"));
            }
        }

        [HttpPost]
        public JsonResult LoginWithFacebook([FromBody] User user)
        {
            if (_context.Users.Where(x => x.FaceBookID == user.FaceBookID).SingleOrDefault() != null)
            {
                return Json(_context.Users.Where(x => x.FaceBookID == user.FaceBookID).SingleOrDefault());
            }

            try
            {
                user.ID = Guid.NewGuid();
                _context.Users.Add(user);
                _context.SaveChanges();
                return Json(user);
            }
            catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpGet("{id}")]
        public JsonResult UpgradeToPro(Guid id)
        {
            try
            {
                User user = _context.Users.Where(x => x.ID == id).SingleOrDefault();
                user.IsPro = true;
                _context.Users.Update(user);
                _context.SaveChanges();
                return Json(Ok());

            } catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpPost]
        public JsonResult EditUserInfo([FromBody] EditableUserInfo newUser)
        {
            User user = _context.Users.Where(x => x.ID == newUser.ID)?.SingleOrDefault();
            if (user != null)
            {
                user.Name = newUser.Name;
                user.Details = newUser.Details;
            }
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return Json(Ok());
            }
            catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpPost]
        public JsonResult EditUserPassword([FromBody] EditableUserInfo newUser)
        {
            User user = _context.Users.Where(x => x.ID == newUser.ID
                    && x.Password == newUser.OldPassword)?.SingleOrDefault();
            if (user != null)
            {
                user.Password = newUser.Password;
            }
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return Json(Ok());
            }
            catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpPost]
        public JsonResult EditUserImage([FromBody] EditableUserImage newImage)
        {
            User user = _context.Users.Where(x => x.ID == newImage.ID)?.SingleOrDefault();
            if (user != null)
            {
                try
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string imageName = Guid.NewGuid() + ".jpg";
                    string imagePath = Path.Combine(path, imageName);
                    byte[] imageBytes = Convert.FromBase64String(newImage.ImageStr);
                    System.IO.File.WriteAllBytes(imagePath, imageBytes);
                    user.ImagePath = imageName;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return Json(Ok(imageName));
                }
                catch (Exception e)
                {
                    return Json(BadRequest(e.Message));
                }
            }
            else
            {
                return Json(BadRequest());
            }
        }
        
    }
}
