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
    public class SpotsController : Controller
    {
        ApplicationDbContext _context { get; set; }
        public SpotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult GetSpotsByPlace([FromBody] SearchModel model)
        {
            #region temp
            var temp = (from p in _context.Places
                        join s in _context.Spots on p.ID equals s.PlaceID
                        join u in _context.Users on p.UserID equals u.ID
                        orderby s.CreationDate descending
                        where s.IsActive && p.ID == model.PlaceID &&
                            (model.isAll || model.isStarred ? !s.IsDone : true)
                        select new
                        {
                            s.ID,
                            s.Title,
                            PlaceTitle = p.Title,
                            UserName = u.Name,
                            s.LowSal,
                            s.HighSal,
                            s.Phone,
                            s.Description,
                            s.Days,
                            s.Minutes,
                            s.IsPM,
                            s.Gender,
                            s.IsDone,
                        }).ToList();
            #endregion
            List<SpotModel> spotModels = new List<SpotModel>();
            foreach (var v in temp)
            {
                bool isStarred = false;
                if (model.isStarred)
                {
                    if (_context.Stareds.Where(x => x.SpotID == v.ID && x.UserID == model.UserID)?
                        .SingleOrDefault() != null)
                    {
                        isStarred = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (_context.Stareds.Where(x => x.SpotID == v.ID && x.UserID == model.UserID)?
                        .SingleOrDefault() != null)
                    {
                        isStarred = true;
                    }
                }
                SpotModel ssMM = new SpotModel();
                ssMM.ID = v.ID;
                ssMM.Title = v.Title;
                ssMM.PlaceTitle = v.PlaceTitle;
                ssMM.UserName = v.UserName;
                ssMM.LowSal = v.LowSal;
                ssMM.HighSal = v.HighSal;
                ssMM.Phone = v.Phone;
                ssMM.Description = v.Description;
                ssMM.Days = v.Days;
                ssMM.Minutes = v.Minutes;
                ssMM.IsPM = v.IsPM;
                ssMM.Gender = v.Gender;
                ssMM.IsDone = v.IsDone;
                ssMM.IsStared = isStarred;
                spotModels.Add(ssMM);
            }
            return Json(spotModels);
        }

        [HttpGet("{id}")] // user ID
        public JsonResult GetMySpots(Guid id)
        {
            var v = (from spot in _context.Spots
                     join place in _context.Places on spot.PlaceID equals place.ID
                     join user in _context.Users on place.UserID equals id
                     orderby spot.CreationDate descending
                     where spot.IsActive
                     select new
                     {
                         spot.ID,
                         spot.Title,
                         PlaceTitle = place.Title,
                         UserName = user.Name,
                         spot.LowSal,
                         spot.HighSal,
                         spot.Phone,
                         spot.Description,
                         spot.Days,
                         spot.Minutes,
                         spot.IsPM,
                         spot.Gender,
                         spot.IsDone
                     }).ToList();
            return Json(v);
        }

        [HttpGet]
        public JsonResult GetSpots()
        {
            return Json((from spot in _context.Spots
                         join place in _context.Places on spot.PlaceID equals place.ID
                         join user in _context.Users on place.UserID equals user.ID
                         orderby spot.CreationDate descending
                         where spot.IsActive && !spot.IsDone
                         select new
                         {
                             spot.ID,
                             spot.Title,
                             PlaceTitle = place.Title,
                             UserName = user.Name,
                             spot.LowSal,
                             spot.HighSal,
                             spot.Phone,
                             spot.Description,
                             spot.Days,
                             spot.Minutes,
                             spot.IsPM,
                             spot.Gender,
                             spot.IsDone,
                             IsStared = false
                         }).ToList());
        }

        [HttpGet("{id}")]
        public JsonResult GetSpots(Guid id)
        {
            var v = (from spot in _context.Spots
                     join place in _context.Places on spot.PlaceID equals place.ID
                     join user in _context.Users on place.UserID equals user.ID
                     orderby spot.CreationDate descending
                     where spot.IsActive && !spot.IsDone && user.ID != id
                     select new
                     {
                         spot.ID,
                         spot.Title,
                         PlaceTitle = place.Title,
                         UserName = user.Name,
                         spot.LowSal,
                         spot.HighSal,
                         spot.Phone,
                         spot.Description,
                         spot.Days,
                         spot.Minutes,
                         spot.IsPM,
                         spot.Gender,
                         spot.IsDone
                     }).ToList();

            List<SpotModel> spotsResults = new List<SpotModel>();
            for (int i = 0; i < v.Count; i++)
            {
                SpotModel spotModel = new SpotModel();
                spotModel.ID = v[i].ID;
                spotModel.Title = v[i].Title;
                spotModel.PlaceTitle = v[i].PlaceTitle;
                spotModel.UserName = v[i].UserName;
                spotModel.LowSal = v[i].LowSal;
                spotModel.HighSal = v[i].HighSal;
                spotModel.Phone = v[i].Phone;
                spotModel.Description = v[i].Description;
                spotModel.Days = v[i].Days;
                spotModel.Minutes = v[i].Minutes;
                spotModel.IsPM = v[i].IsPM;
                spotModel.Gender = v[i].Gender;
                var x = _context.Stareds.Where(y => y.SpotID == v[i].ID && y.UserID == id)?.SingleOrDefault();
                if (x != null)
                {
                    spotModel.IsStared = true;                    
                }
                else
                {
                    spotModel.IsStared = false;
                }
                spotsResults.Add(spotModel);
            }

            return Json(spotsResults);
        }

        [HttpGet("{id}")]
        public Spot GetSpot(Guid id)
        {
            return _context.Spots
                .Where(x => (x.ID == id) &&
                    x.IsActive && !x.IsDone)
                .SingleOrDefault();
        }

        [HttpPost]
        public JsonResult AddSpot([FromBody] Spot spot)
        {
            try
            {
                spot.ID = Guid.NewGuid();
                spot.IsDone = false;
                spot.IsActive = true;
                _context.Spots.Add(spot);
                _context.SaveChanges();
                return Json(Ok());
            }
            catch (Exception e) 
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpGet("{id}")]
        public JsonResult DeleteSpot(Guid id)
        {
            try
            {
                Spot spot = _context.Spots.Where(x => x.ID == id).SingleOrDefault();
                spot.IsActive = false;
                _context.Spots.Update(spot);
                _context.SaveChanges();
                return Json(Ok());
            } catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpGet("{id}")]
        public JsonResult ToggleDoneSpot(Guid id)
        {
            try
            {
                Spot spot = _context.Spots.Where(x => x.ID == id).SingleOrDefault();
                if (spot.IsDone)
                {
                    spot.IsDone = false;
                }
                else
                {
                    spot.IsDone = true;
                }
                _context.Spots.Update(spot);
                _context.SaveChanges();
                return Json(Ok());
            }
            catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpPost]
        public JsonResult EditSpot([FromBody] Spot spot)
        {
            try
            {
                spot.IsActive = true;
                spot.CreationDate = DateTime.Now;
                _context.Spots.Update(spot);
                _context.SaveChanges();
                return Json(Ok());
            }
            catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }

        [HttpGet("{id}")] 
        public List<Spot> GetStaredSpots(Guid id)
        {
            var MyStared = _context.Stareds.
                Where(x => x.UserID == id).ToList();

            List<Spot> MyStaredSpots = new List<Spot>();
            foreach(Stared s in MyStared)
            {
                var stared = _context.Spots.Where(y => y.ID == s.SpotID && y.IsActive).SingleOrDefault();
                if (stared != null)
                {
                    MyStaredSpots.Add(stared);
                }
            }
            return MyStaredSpots;
        }

        [HttpPost]
        public JsonResult ToggleStaredSpot([FromBody] Stared stared)
        {
            try
            {
                Stared ss = _context.Stareds
                    .Where(x => x.SpotID == stared.SpotID && x.UserID == stared.UserID)?
                    .SingleOrDefault();

                if (ss == null)
                {
                    stared.ID = Guid.NewGuid();
                    _context.Stareds.Add(stared);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Stareds.Remove(ss);
                    _context.SaveChanges();
                }
                return Json(Ok());
            } catch (Exception e)
            {
                return Json(BadRequest(e.Message));
            }
        }
    }
}
