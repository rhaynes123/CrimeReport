using System;
using CrimeReport.Features.Crimes;

namespace CrimeReport.Features.Laws
{
    /// <summary>
    /// This repository is to be used only to read data about laws
    /// </summary>
    public interface IReadLawRepository
    {
        /// <summary>
        /// Gets All Laws
        /// </summary>
        /// <param name="useCache"> optional paramter to bypass distrubted cache. This has no effect on any database cache</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Lis of Laws</returns>
        Task<IList<Law>> GetAllAsync(bool useCache = true, CancellationToken cancellationToken = default);
    }
}

