using MicroondasBennerAPI.Models.Base;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Service.Contracts;

namespace MicroondasBennerAPI.Service.Services
{
    public class ProgramasPersonalizadosService(IProgramasPersonalizadosRepository programasPersonalizadosRepository) : IProgramasPersonalizadosService
    {
        private readonly IProgramasPersonalizadosRepository _programasPersonalizadosRepository = programasPersonalizadosRepository;

        public async Task<bool> DeleteAsync(int id)
            => await _programasPersonalizadosRepository.DeleteAsync(id);

        public async Task<IEnumerable<ProgramaAquecimento>> GetAllAsync()
            => await _programasPersonalizadosRepository.GetAllAsync();

        public async Task<ProgramaAquecimento?> GetByIdAsync(int id)
            => await _programasPersonalizadosRepository.GetByIdAsync(id);

        public async Task<IEnumerable<ProgramaAquecimento?>> GetByUserIdAsync(int id)
            => await _programasPersonalizadosRepository.GetByUserIdAsync(id);

        public async Task<int> InsertAsync(ProgramaAquecimento programa)
        {
            await ValidarSimboloJaExiste(programa.SimboloProgresso);

            return await _programasPersonalizadosRepository.InsertAsync(programa);
        }

        private async Task ValidarSimboloJaExiste(char simboloProgresso)
        {
            if (SimboloUtilizadoProgramaPreCadastrado(simboloProgresso)
                || await SimboloUtilizadoProgramaPersonalizadoAsync(simboloProgresso))
                    throw new ApplicationException("Símbolo já utilizado.");
        }

        private async Task<bool> SimboloUtilizadoProgramaPersonalizadoAsync(char simbolo)
            => await _programasPersonalizadosRepository.SimboloUtilizadoProgramaPersonalizadoAsync(simbolo);

        public async Task<bool> UpdateAsync(ProgramaAquecimento programa)
        {
            await ValidarSimboloJaExiste(programa.SimboloProgresso);

            return await _programasPersonalizadosRepository.UpdateAsync(programa);
        }

        //Poderia ser feito de forma mais simples, mas preferi usar Reflection, justamente para mostrar a técnica
        private static bool SimboloUtilizadoProgramaPreCadastrado(char simbolo)
        {
            var tipos = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch
                    {
                        return Array.Empty<Type>();
                    }
                })
                .Where(t => t.IsSubclassOf(typeof(ProgramaAquecimento)) && !t.IsAbstract);

            foreach (var tipo in tipos)
            {
                var instancia = (ProgramaAquecimento)Activator.CreateInstance(tipo)!;

                if (instancia.SimboloProgresso == simbolo)
                    return true;
            }

            return false;
        }
    }
}
