﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelBookingPlatform.Infrastructure.Persistence.DbContexts;

#nullable disable

namespace TravelBookingPlatform.Infrastructure.Migrations
{
    [DbContext(typeof(HotelBookingDbContext))]
    partial class HotelBookingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AmenityRoomCategory", b =>
                {
                    b.Property<Guid>("AmenitiesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomsCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AmenitiesId", "RoomsCategoryId");

                    b.HasIndex("RoomsCategoryId");

                    b.ToTable("AmenityRoomCategory");
                });

            modelBuilder.Entity("BookingRoom", b =>
                {
                    b.Property<Guid>("BookingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomsRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookingsId", "RoomsRoomId");

                    b.HasIndex("RoomsRoomId");

                    b.ToTable("BookingRoom");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Amenity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("BookingDateUtc")
                        .HasColumnType("date");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<DateOnly>("CheckInDateUtc")
                        .HasColumnType("date");

                    b.Property<DateOnly>("CheckOutDateUtc")
                        .HasColumnType("date");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GuestRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("HotelId");

                    b.HasIndex("CheckInDateUtc", "CheckOutDateUtc");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostOffice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Percentage")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("RoomCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoomCategoryId");

                    b.HasIndex("StartDateUtc", "EndDateUtc");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Hotel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BriefDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("float(8)");

                    b.Property<double>("Longitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("float(8)");

                    b.Property<DateTime?>("ModifiedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ReviewsRating")
                        .HasPrecision(8, 6)
                        .HasColumnType("float(8)");

                    b.Property<int>("StarRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("StarRating");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.InvoiceRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("DiscountPercentageAtBooking")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PriceAtBooking")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoomCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("RoomId");

                    b.ToTable("InvoiceRecord");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("HotelId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3d736b28-cf80-4dc1-8e49-453e3760f0be"),
                            Description = "Guest role description",
                            Name = "Guest",
                            UserRole = 0
                        },
                        new
                        {
                            Id = new Guid("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b"),
                            Description = "Admin role description",
                            Name = "Admin",
                            UserRole = 1
                        },
                        new
                        {
                            Id = new Guid("b3e735b8-cf80-4dc1-8e49-453e3760f0be"),
                            Description = "Manager role description",
                            Name = "Manager",
                            UserRole = 2
                        });
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoomCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomCategoryId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.RoomCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AdultsCapacity")
                        .HasColumnType("int");

                    b.Property<int>("ChildrenCapacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerNight")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("PricePerNight");

                    b.HasIndex("RoomType");

                    b.HasIndex("AdultsCapacity", "ChildrenCapacity");

                    b.ToTable("RoomCategories");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b0a3e9b6-2b47-4fbd-9a9d-5c7c07fbcf45"),
                            Email = "admin@travelbooking.com",
                            FirstName = "Admin",
                            LastName = "-",
                            Password = "ALaU/k/AAn4cw05diU6Bb0Va6ZCQt2dDewGrngK3ez2i4TBtRCSEzIgStvbVeRZB8A=="
                        },
                        new
                        {
                            Id = new Guid("ce4b3194-6d9f-4b72-a15d-42b505a9b4ad"),
                            Email = "manager@travelbooking.com",
                            FirstName = "manager",
                            LastName = "-",
                            Password = "$2a$12$7cQg8.ZpsjO75AqKScINhux2QX50sL9VYXvB3oMM2Bh.1TV2Aq3NO"
                        });
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b"),
                            UserId = new Guid("b0a3e9b6-2b47-4fbd-9a9d-5c7c07fbcf45")
                        });
                });

            modelBuilder.Entity("AmenityRoomCategory", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.Amenity", null)
                        .WithMany()
                        .HasForeignKey("AmenitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelBookingPlatform.Domain.Entities.RoomCategory", null)
                        .WithMany()
                        .HasForeignKey("RoomsCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookingRoom", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelBookingPlatform.Domain.Entities.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Booking", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.User", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelBookingPlatform.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Bookings")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Discount", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.RoomCategory", "RoomCategory")
                        .WithMany("Discounts")
                        .HasForeignKey("RoomCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomCategory");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Hotel", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.City", "City")
                        .WithMany("Hotels")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelBookingPlatform.Domain.Entities.Owner", "Owner")
                        .WithMany("Hotels")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.InvoiceRecord", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.Booking", "Booking")
                        .WithMany("Invoice")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelBookingPlatform.Domain.Entities.Room", null)
                        .WithMany("InvoiceRecords")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Review", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.User", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelBookingPlatform.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Reviews")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Room", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.RoomCategory", "RoomCategory")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RoomCategory");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.RoomCategory", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.Hotel", "Hotel")
                        .WithMany("RoomCategory")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("TravelBookingPlatform.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelBookingPlatform.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Booking", b =>
                {
                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.City", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Hotel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Reviews");

                    b.Navigation("RoomCategory");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Owner", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.Room", b =>
                {
                    b.Navigation("InvoiceRecords");
                });

            modelBuilder.Entity("TravelBookingPlatform.Domain.Entities.RoomCategory", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
