using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Cinema.Core.Enums;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.Data.Seed
{
    public class CinemaContextDataSeeder
    {
        private readonly UserManager<CinemaUser> userManager;
        private readonly CinemaContext context;

        public CinemaContextDataSeeder(UserManager<CinemaUser> userManager, CinemaContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task SeedAsync()
        {
            var roles = new List<IdentityRole<int>>
            {
                new()
                {
                    Name = nameof(CinemaRoles.Admin),
                    NormalizedName = nameof(CinemaRoles.Admin).ToUpperInvariant()
                },
                new()
                {
                    Name = nameof(CinemaRoles.Customer),
                    NormalizedName = nameof(CinemaRoles.Customer).ToUpperInvariant()
                },
                new()
                {
                    Name = nameof(CinemaRoles.Employee),
                    NormalizedName = nameof(CinemaRoles.Employee).ToUpperInvariant()
                }
            };

            context.Roles.UpsertRange(roles).On(x => x.Name).Run();

            var existingAdmin = await userManager.FindByEmailAsync("cinema@gmail.com");
            if (existingAdmin is null)
            {
                var newUser = new CinemaUser
                {
                    Email = "cinema@gmail.com",
                    UserName = "Cinema",
                    EmailConfirmed = true,
                    DateCreated = DateTime.UtcNow
                };
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var createdUser = await userManager.CreateAsync(newUser, "Cinema321123");

                if (!createdUser.Succeeded)
                {
                    var messages = createdUser.Errors.Select(x => x.Description);
                    throw new Exception(string.Join(";", messages));
                }

                var createdRoles = await userManager.AddToRolesAsync(newUser, new List<string> { nameof(CinemaRoles.Admin) });
                if (!createdRoles.Succeeded)
                {
                    var messages = createdRoles.Errors.Select(x => x.Description);
                    throw new Exception(string.Join(";", messages));
                }

                scope.Complete();
            }

            var existingUser = await userManager.FindByEmailAsync("user@gmail.com");
            if (existingUser is null)
            {
                var newUser = new CinemaUser
                {
                    Email = "user@gmail.com",
                    UserName = "User",
                    EmailConfirmed = true,
                    DateCreated = DateTime.UtcNow
                };
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var createdUser = await userManager.CreateAsync(newUser, "User321123");

                if (!createdUser.Succeeded)
                {
                    var messages = createdUser.Errors.Select(x => x.Description);
                    throw new Exception(string.Join(";", messages));
                }

                var createdRoles = await userManager.AddToRolesAsync(newUser, new List<string> { nameof(CinemaRoles.Customer) });
                if (!createdRoles.Succeeded)
                {
                    var messages = createdRoles.Errors.Select(x => x.Description);
                    throw new Exception(string.Join(";", messages));
                }

                existingUser = await userManager.FindByEmailAsync("user@gmail.com");
                scope.Complete();
            }

            var h = await context.Halls.SingleOrDefaultAsync(x => x.Name == "Big hall");
            if (h is null)
            {
                context.Halls.Add(new HallEntity
                {
                    Name = "Big hall",
                    Rows = 10,
                    SeatsPerRow = 8,
                    Screenings = new List<ScreeningEntity>
                    {
                        new ScreeningEntity
                        {
                            Date = DateTime.UtcNow.AddDays(5),
                            Price = 20,
                            Movie = new MovieEntity
                            {
                                Description = "Example description of super scary movie",
                                Length = new TimeSpan(1, 30, 50),
                                Title = "Super scary movie",
                                PictureUrl = "https://i.guim.co.uk/img/static/sys-images/Technology/Pix/columnists/2011/6/7/1307461877809/In-with-the-Flynns-007.jpg?width=620&quality=85&auto=format&fit=max&s=5af55926855ad64ba2cd5497fb2cc51a"
                            },
                            Bookings = new List<BookingEntity>
                            {
                                new BookingEntity
                                {
                                    Date = DateTime.UtcNow,
                                    IsConfirmed = true,
                                    Row = 6,
                                    Seat = 7,
                                    UserId = existingUser.Id,
                                }
                            }
                        }
                    }
                });
            }

            h = await context.Halls.SingleOrDefaultAsync(x => x.Name == "Small hall");
            if (h is null)
            {
                context.Halls.Add(new HallEntity
                {
                    Name = "Small hall",
                    Rows = 4,
                    SeatsPerRow = 5,
                    Screenings = new List<ScreeningEntity>
                    {
                        new ScreeningEntity
                        {
                            Date = DateTime.UtcNow.AddDays(2),
                            Price = 15,
                            Movie = new MovieEntity
                            {
                                Description = "Clowns family comedy",
                                Length = new TimeSpan(1, 40, 20),
                                Title = "Family comedy",
                                PictureUrl = "https://cdn.shopify.com/s/files/1/0548/8404/0870/products/TheFamilyComedy-PersonalizedMoviePoster_b59527ba-7360-415b-98b3-33b4086e7c06_5000x.jpg?v=1617385439"
                            },
                            Bookings = new List<BookingEntity>
                            {
                                new BookingEntity
                                {
                                    Date = DateTime.UtcNow.AddDays(-1).AddHours(-2).AddMinutes(-20),
                                    IsConfirmed = false,
                                    Row = 2,
                                    Seat = 3,
                                    UserId = existingUser.Id,
                                }
                            }
                        }
                    }
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
