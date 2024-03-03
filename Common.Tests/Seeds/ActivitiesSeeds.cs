﻿using Microsoft.EntityFrameworkCore;
using project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Common.Tests.Seeds
{
    // Class containing seed data for activities
    public static class ActivitiesSeeds
    {
        // Seed data for activities
        public static readonly ActivityEntity ICSCviko = new()
        {

            Id = Guid.Parse("c7435c8b-30a1-4fc9-8e0b-36b3ff2ee8c5"),
            Area = Enums.SchoolArea.ComputerLab,
            BeginTime = DateTime.Now,
            Description = "ICS Cvičenie",
            EndTime = DateTime.Now.AddHours(2),
            Subject = SubjectSeeds.ICS,
            SubjectId = SubjectSeeds.ICS.Id,
            Type = Enums.ActivityType.Exercise,
            Ratings = new List<RatingEntity>()
        };
        public static readonly ActivityEntity IOSPolsemka = new()
        {
            Id = Guid.Parse("d0de8b41-9028-4f5f-82d8-7a3ae1bf8b18"),
            Area = Enums.SchoolArea.MainLectureHall,
            BeginTime = DateTime.Now,
            Description = "IOS Polsemestralka",
            EndTime = DateTime.Now.AddHours(2),
            Subject = SubjectSeeds.IOS,
            SubjectId = SubjectSeeds.IOS.Id,
            Type = Enums.ActivityType.MidtermExam,
            Ratings = new List<RatingEntity>()
        };

        static ActivitiesSeeds() // Initialize seed data for the subjects
        {
            ActivitiesSeeds.ICSCviko.Ratings.Add(RatingsSeeds.ICSRating);
            ActivitiesSeeds.IOSPolsemka.Ratings.Add(RatingsSeeds.IOSRating);
        }

        // Seed activity data into the model builder
        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<ActivityEntity>().HasData(ICSCviko, IOSPolsemka);
    }
}
