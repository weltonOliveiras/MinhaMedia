/***********************************************************************
 *          Nome: Aluno
 *          obs.: Representa a classe de objetos. Classe com atributos 
 *                privados e métodos (Get/Set) Públicos
 *   Dt. Criação: 10/02/2023
 * Dt. Alteração: --
 *    Criada por: WeltonOliveira
 * *********************************************************************/

namespace Sys_MinhaMedia
{
    public class Aluno
    {
        ~Aluno()
        {

        }

        #region Atributos Privados
        private int v_Cod_Aluno = -1;
        private int v_Cod_Turma = -1;
        private string v_Nm_Aluno = "";
        private int v_Nmr_Aluno = -1;
        #endregion

        #region Metodos Públicos
        public int Cod_Aluno
        {
            get => v_Cod_Aluno;
            set => v_Cod_Aluno = value;
        }

        public int Cod_Turma
        {
            get => v_Cod_Turma;
            set => v_Cod_Turma = value;
        }

        public string Nm_Aluno
        {
            get => v_Nm_Aluno;
            set => v_Nm_Aluno = value;
        }
        

        public int Nmr_Aluno
        {
            get => v_Nmr_Aluno;
            set => v_Nmr_Aluno = value;
        }

        #endregion
    }
}
