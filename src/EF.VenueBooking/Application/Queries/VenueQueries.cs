using EF.VenueBooking.Application.ViewModels;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF.VenueBooking.Application.Queries
{
    public interface VenueQueries
    {
        Task<Option<VenueViewModel>> Find(Guid id);
    }
}
