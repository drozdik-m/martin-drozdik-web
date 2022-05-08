using MartinDrozdik.Abstraction.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MartinDrozdik.Web.Admin.Client.Components.CExtra
{
    public partial class SeedsCaller : RootComponent
    {
        [Inject]
        ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Indicates that any loading is in progress
        /// </summary>
        protected bool AnyLoading => seedLoading || seedAllLoading;

        /// <summary>
        /// Seeds to handle
        /// </summary>
        [Parameter]
        public IEnumerable<(string Name, ISeedableService Seed)> Seeds { get; set; }

        #region Error
        /// <summary>
        /// The last error the occurred
        /// </summary>
        Exception? lastException = default;
        #endregion

        #region Seed
        protected bool seedLoading = false;
        protected bool seedAllLoading = false;
        int seedAllProgress = -1;

        /// <summary>
        /// Executes all seeds
        /// </summary>
        /// <returns></returns>
        public async Task SeedAllAsync()
        {
            try
            {
                seedAllLoading = true;
                foreach(var (_, seed) in Seeds)
                    await seed.SeedAsync();
                seedAllLoading = false;

                StateHasChanged();

                Snackbar.Add("Seeding successful", Severity.Success);

                lastException = null;
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
            }
            finally
            {
                seedAllLoading = false;
            }
        }

        /// <summary>
        /// Executes a seed
        /// </summary>
        /// <param name="service"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public async Task SeedAsync(ISeedableService service, bool verbose = true)
        {
            try
            {
                seedLoading = true;
                await service.SeedAsync();
                seedLoading = false;

                StateHasChanged();

                if (verbose)
                    Snackbar.Add("Seeding successful", Severity.Success);

                lastException = null;
            }
            catch (Exception ex)
            {
                lastException = ex;
                Snackbar.Add("Something went wrong :(", Severity.Error);
            }
            finally
            {
                seedLoading = false;
            }
        }
        #endregion

       
    }
}
