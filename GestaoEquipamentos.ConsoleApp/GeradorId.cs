namespace GestaoEquipamentos.ConsoleApp
{
    public class GeradorId
    {
        private static int idEquipamento = 0;

        private static int idChamado = 0;

        private static int idSolicitante = 0;

        public static int GerarIdEquipamento()
        {
            return ++idEquipamento;
        }

        public static int GerarIdChamado()
        {
            return ++idChamado;
        }

        public static int GerarIdSolicitante()
        {
            return ++idSolicitante;
        }
    }
}