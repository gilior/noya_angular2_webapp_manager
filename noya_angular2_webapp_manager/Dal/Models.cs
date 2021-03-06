﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noya_angular2_webapp_manager.Dal
{


    public class UpdateDataRequest
    {


    }
    public class UpdateMenuRequest : UpdateDataRequest
    {
        public MenuItem[] MenuItems { get; set; }

    }
    public class UpdateProgramsRequest : UpdateDataRequest
    {
        public Program[] Programs { get; set; }

    }
    public class UpdateCalendarRequest : UpdateDataRequest
    {
        public CalendarItem[] CalendarItems { get; set; }

    }

    public class UpdateUpdatesRequest : UpdateDataRequest
    {
        public Update[] Updates { get; set; }

    }

    public class UpdateImagesRequest : UpdateDataRequest
    {
        public ImageGalleryItem[] Images { get; set; }

    }

    public class UpdatePressRequest : UpdateDataRequest
    {
        public PressItem[] PressItems { get; set; }

    }
    public class UpdateLinksRequest : UpdateDataRequest
    {
        public Link[] Links { get; set; }

    }
    public class UpdateCVRequest : UpdateDataRequest
    {
        public CV[] CVs { get; set; }

    }


    public class DataRequest
    {


    }

    public class UpdateRsponse : DataRespone
    {
        public Update[] Updates { get; set; }
    }

    public class Update
    {
        public int ID { get; set; }
        public string Data_Heb { get; set; }
        public string Data_Eng { get; set; }

        public double Order { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool ToDelete { get; set; }

    }

    public class PressResponse : DataRespone
    {
        public PressItem[] PressItems { get; set; }
    }



    public class ImageGalleryItem
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }
        public string ImageURL { get; set; }
        public string ImageID { get; set; }
        public double Order { get; set; }
        public bool Visible { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool ToDelete { get; set; }
        public bool IsNew { get; set; }

    }



    public class ImageGalleryResponse : DataRespone
    {
        public ImageGalleryItem[] Images { get; set; }

    }

    public class ImageGalleryRequest : DataRequest
    {

    }

    public class PressItem
    {
        public int ID { get; set; }
        public string Heb { get; set; }
        public string Eng { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool ToDelete { get; set; }

    }


    public class DataRespone
    {

    }

    public enum UpdateStatus
    {
        Success, Fail
    }

    public class UpdateResponse
    {

        public UpdateStatus UpdateStatus { get; set; }
    }

    public class Program
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Heb { get; set; }
        public string Eng { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Order { get; set; }
        public bool ToDelete { get; set; }

    }

    public class ProgramsResponse : DataRespone
    {
        public Program[] Programs { get; set; }
    }

    public class CVResponse : DataRespone
    {
        public CV[] CVs { get; set; }
    }

    public class CalendarRequset : DataRequest
    {


    }

    public class CalendarResponse : DataRespone
    {
        public CalendarItem[] CalendarItems { get; set; }
    }

    public class CalendarItem
    {
        public string Text_Heb { get; set; }
        public string Text_Eng { get; set; }
        public bool Visible { get; set; }
        public DateTime TimeStamp { get; set; }

        public DateTime DataDate { get; set; }
        public string DataDateString { get; set; }
        public int ID { get; set; }
        public bool ToDelete { get; set; }
    }

    public class CV
    {
        public int ID { get; set; }

        public string Heb { get; set; }
        public string Eng { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool ToDelete { get; set; }

    }

    public class LinksResponse : DataRespone
    {
        public Link[] Links { get; set; }
    }

    public class MenuResponse : DataRespone
    {
        public MenuItem[] MenuItems { get; set; }
    }

    public struct Link
    {


        public int ID { get; set; }
        public string Text_Heb { get; set; }
        public string Address_Heb { get; set; }
        public string Text_Eng { get; set; }
        public string Address_Eng { get; set; }
        public double Order { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool ToDelete { get; set; }


        public Link(int id, string text_Heb, string address_Heb, string text_Eng, string address_Eng, double order, DateTime timeStamp, bool toDelete)
        {
            this.ID = id;
            this.Text_Heb = text_Heb;
            this.Address_Heb = address_Heb;
            this.Address_Eng = address_Eng;
            this.Order = order;
            this.TimeStamp = timeStamp;
            this.Text_Eng = text_Eng;
            this.ToDelete = toDelete;
        }


    }
    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
    public class MessageResponse : DataRespone
    {

    }

    public class Message
    {
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Person Sender { get; set; }
        public string IP { get; set; }
    }



    public struct MenuItem
    {
        public Boolean ToDelete;
        public int ID;
        public double Order;
        public string Text_English;
        public string Text_Hebrew;
        public string Name;
        public bool isDefault;
        public int ImageID { get; set; }
        public MenuItem(int id, double order, string text_English, string text_Hebrew, bool isDefault, string name, int imageID)
        {
            this.ID = id;
            this.Order = order;
            this.Text_English = text_English;
            this.Text_Hebrew = text_Hebrew;
            this.isDefault = isDefault;
            this.ToDelete = false;
            this.Name = name;
            this.ImageID = imageID;
        }

    }
}