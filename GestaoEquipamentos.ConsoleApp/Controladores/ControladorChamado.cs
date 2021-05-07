using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Controladores
{
    public class ControladorChamado : ControladorBase
    {
        private ControladorEquipamento controladorEquipamento;
        private ControladorSolicitante controladorSolicitante;

        public ControladorChamado(ControladorEquipamento controlador, ControladorSolicitante controlador2)
        {
            controladorEquipamento = controlador;
            controladorSolicitante = controlador2;
        }

        public string RegistrarChamado(int id, int idEquipamentoChamado, int idSolicitanteChamado,
            string titulo, string descricao, DateTime dataAbertura)
        {
            Chamado chamado = null;

            int posicao;

            if (id == 0)
            {
                chamado = new Chamado();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Chamado(id));
                chamado = (Chamado)registros[posicao];
            }

            chamado.equipamento = controladorEquipamento.SelecionarEquipamentoPorId(idEquipamentoChamado);
            chamado.solicitante = controladorSolicitante.SelecionarSolicitantePorId(idSolicitanteChamado);
            chamado.titulo = titulo;
            chamado.descricao = descricao;
            chamado.dataAbertura = dataAbertura;

            string resultadoValidacao = chamado.Validar();

            if (resultadoValidacao == "CHAMADO_VALIDO")
                registros[posicao] = chamado;

            return resultadoValidacao;
        }

        public bool ExcluirChamado(int idSelecionado)
        {
            return ExcluirRegistro(new Chamado(idSelecionado));
        }



        public int EditarSolicitante(int idchamado, string nome, string email,
    string telefone)
        {
            Chamado chamado = SelecionarChamadoPorId(idchamado);
            int idSolicitante = chamado.solicitante.id;
            controladorSolicitante.RegistrarSolicitante(idSolicitante, nome, email, telefone);
            return idSolicitante;
        }
        public Chamado SelecionarChamadoPorId(int id)
        {
            return (Chamado)SelecionarRegistroPorId(new Chamado(id));
        }
        public Chamado[] SelecionarTodosChamados()
        {
            Chamado[] chamadosAux = new Chamado[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), chamadosAux, chamadosAux.Length);

            return chamadosAux;
        }


    }
}