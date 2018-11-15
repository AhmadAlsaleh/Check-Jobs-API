using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheckJobsAPI.Data;
using CheckJobsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckJobsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PlacesController : Controller
    {
        ApplicationDbContext _context { get; set; }
        public PlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAllPlaces()
        {
            var allPlaces =
            (from spot in _context.Spots
             join place in _context.Places on spot.PlaceID equals place.ID
             join user in _context.Users on place.UserID equals user.ID
             where spot.IsActive && !spot.IsDone
                 && place.IsActive
             select new
             {
                 place.ID,
                 place.Title,
                 place.Latitude,
                 place.Longitude,
				 UserID = user.ID,
                 user.Name
             });
            return Json(allPlaces.GroupBy(x => x.ID).Select(x => x.First()));
        }

        [HttpGet("{id}")]
        public JsonResult GetMyStarredPlaces(Guid id)
        {
            return Json(
                (from starred in _context.Stareds
                 join spot in _context.Spots on starred.SpotID equals spot.ID
                 join place in _context.Places on spot.PlaceID equals place.ID
                 join user in _context.Users on place.UserID equals user.ID
                 where starred.UserID == id &&
                    spot.IsActive && !spot.IsDone &&
                    place.IsActive
                 select new
                 {
                     place.ID,
                     place.Title,
                     place.Latitude,
                     place.Longitude,
                     UserID = user.ID,
                     user.Name
                 }).ToList());
        }

        [HttpGet("{id}")]
        public JsonResult GetMyMapPlaces(Guid id)
        {
            return Json(
                (from spot in _context.Spots
                 join place in _context.Places on spot.PlaceID equals place.ID
                 join user in _context.Users on place.UserID equals user.ID
                 where spot.IsActive &&
                    place.IsActive && place.UserID == id
                 select new
                 {
                     place.ID,
                     place.Title,
                     place.Latitude,
                     place.Longitude,
                     UserID = user.ID,
                     user.Name
                 }).ToList());
        }

        [HttpGet("{id}")]
        public List<Place> GetMyAllPlaces(Guid id) 
        {
            return _context.Places
                .Where(x => x.UserID == id && x.IsActive)
                .OrderByDescending(y => y.CreationDate)
                .ToList();
        }

        [HttpPost]
        public JsonResult GetFilterredPlaces([FromBody] FilterModel model)
        {
            var temp = (from spot in _context.Spots
                        join place in _context.Places on spot.PlaceID equals place.ID
                        join user in _context.Users on place.UserID equals user.ID
                        where spot.IsActive && !spot.IsDone
                            && place.IsActive
                            && spot.Title.Contains(model.Title.Trim())
                            && (model.Gender != null ? spot.Gender == model.Gender : true)
                            && (model.LowSal != null ? spot.LowSal >= model.LowSal : true)
                            && (model.HighSal != null ? spot.HighSal >= model.HighSal : true)
                            && (model.IsPM != null ? spot.IsPM == model.IsPM : true)
                            && (model.days != null ? spot.Days <= model.days : true)
                            && (model.Minutes != null ? spot.Minutes <= model.Minutes : true)
                        select new
                        {
                            place.ID,
                            place.Title,
                            place.Latitude,
                            place.Longitude,
                            UserID = user.ID,
                            user.Name
                        }).ToList();
            return Json(temp);
        }

        [HttpGet("{id}")]
        public JsonResult DeletePlace(Guid id) 
        {
            try
            {
                Place place = _context.Places
                    .Where(x => x.ID == id)?.SingleOrDefault();
                place.IsActive = false;
                _context.Places.Update(place);
                _context.SaveChanges();
                var spots = _context.Spots.Where(x => x.PlaceID == id).ToList();
                foreach (Spot spot in spots)
                {
                    spot.IsActive = false;
                    _context.Spots.Update(spot);
                    _context.SaveChanges();
                }
                return Json(Ok());
            } catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpPost]
        public JsonResult AddPlace([FromBody] Place place)
        {
            try
            {
                place.ID = Guid.NewGuid();
                place.IsActive = true;
                place.CreationDate = DateTime.Now;
                _context.Places.Add(place);
                _context.SaveChanges();
                return Json(Ok());
            } catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }

        }

        [HttpPost]
        public JsonResult EditPlace([FromBody] Place place)
        {
            try
            {
                place.IsActive = true;
                place.CreationDate = DateTime.Now;
                _context.Places.Update(place);
                _context.SaveChanges();
                return Json(Ok());
            }
            catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

    }
}