using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaChamado : TelaBase
    {
        private TelaEquipamento telaEquipamento;
        private ControladorChamado controladorChamado;
        private TelaSolicitante telaSolicitante;

        public TelaChamado(TelaEquipamento tela, ControladorChamado controlador, TelaSolicitante tela2)
            : base("Cadastro de Chamados")
        {
            telaEquipamento = tela;
            controladorChamado = controlador;
            telaSolicitante = tela2;
        }

        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo chamado...");

            bool conseguiuGravar = GravarChamado(0);

            if (conseguiuGravar)
                ApresentarMensagem("Chamado inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o chamado", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um chamado...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do chamado que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorChamado.ExcluirChamado(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Chamado excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o chamado", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public override void EditarRegistro()
        {
            ConfigurarTela("Editando um chamado...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do chamado que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuEditar = GravarChamado(idSelecionado);

            if (conseguiuEditar)
                ApresentarMensagem("Chamado editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o chamado", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public override void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando chamados...");

            MontarCabecalhoTabela();

            Chamado[] chamados = controladorChamado.SelecionarTodosChamados();

            if (chamados.Length == 0)
            {
                ApresentarMensagem("Nenhum chamado registrado!", TipoMensagem.Atencao);
                return;
            }

            foreach (Chamado chamado in chamados)
            {
                //  Console.WriteLine("{0,-10} | {1,-20} | {2,-20} | {3,-25}",
     //chamado.id, chamado.equipamento.nome, chamado.titulo, chamado.DiasEmAberto);
                 Console.WriteLine("{0,-10} | {1,-30} | {2,-30} | {3,-20} | {4,-25}",
                   chamado.id, chamado.solicitante.nome, chamado.equipamento.nome, chamado.titulo, chamado.DiasEmAberto);
            }
        }

        #region Métodos Privados
        private static void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            //Console.WriteLine("{0,-10} | {1,-30} | {2,-30} | {3,-20} | {4,-25}", "Id", "Equipamento", "Título", "Dias em Aberto");

            Console.WriteLine("{0,-10} | {1,-30} | {2,-30} | {3,-20} | {4,-25}", "Id","Solicitante", "Equipamento", "Título", "Dias em Aberto");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarChamado(int idChamadoSelecionado)
        {
            int idSolicitanteChamado=-1;
            if (idChamadoSelecionado != 0)
            {
                Console.Write("Deseja editar o solicitante desse chamado? Digite 1 para sim, qualquer outra coisa para não: ");
                string editSol = Console.ReadLine();
                if (editSol == "1")
                {
                    Console.Write("Digite o nome do solicitante: ");
                    string nome = Console.ReadLine();

                    Console.Write("Digite o email do solicitante: ");
                    string email = Console.ReadLine();

                    Console.Write("Digite o telefone do solicitante: ");
                    string telefone = Console.ReadLine();
                    idSolicitanteChamado = controladorChamado.EditarSolicitante(idChamadoSelecionado, nome, email, telefone);
                }
            }
            if (idSolicitanteChamado==-1)
            {
                telaSolicitante.VisualizarRegistros();

                Console.Write("Digite o Id do solicitante para manutenção: ");
                idSolicitanteChamado = Convert.ToInt32(Console.ReadLine());
            }



            telaEquipamento.VisualizarRegistros();

            Console.Write("Digite o Id do equipamento para manutenção: ");
            int idEquipamentoChamado = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o titulo do chamado: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descricao do chamado: ");
            string descricao = Console.ReadLine();

            Console.Write("Digite a data de abertura do chamado: ");
            DateTime dataAbertura = Convert.ToDateTime(Console.ReadLine());

            string resultadoValidacao = controladorChamado.RegistrarChamado(idChamadoSelecionado, 
                idEquipamentoChamado, idSolicitanteChamado, titulo, descricao, dataAbertura);

            bool conseguiuGravar = true;

            if (resultadoValidacao != "CHAMADO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        #endregion
    }
}
