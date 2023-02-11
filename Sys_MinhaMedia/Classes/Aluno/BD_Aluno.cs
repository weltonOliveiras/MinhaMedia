/***********************************************************************
 *          Nome: BDAluno
 *          obs.: Representa a classe de Banco de Dados Aluno. 
 *                A Classe utiliza o objeto Connection para acessar o BD
 *   Dt. Criação: 11/02/2023
 * Dt. Alteração: --
 *    Criada por: WeltonOliveira
 * *********************************************************************/
using Sys_MinhaMedia;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sys_MinhaMedia
{
    class BDAluno
    {
        //Destructor da Classe
        ~BDAluno()
        {

        }

        /***********************************************************************
         *        Método: Incluir
         *     Parametro: Objeto Aluno
         *          Obs.: Responsável por incluir um registro na tabela Aluno.
         *   Dt. Criação: 11/02/2023
         * Dt. Alteração: --
         *    Criada por: WeltonOliveira
         ***********************************************************************/
        public int Incluir(Aluno pobj_Aluno)
        {
            // Criar o objeto de conexão com o banco.
            SqlConnection obj_Con = new SqlConnection(Connection.ConectionPath());

            //(19/05/2022 - mFacine) A instrução de comando da SQL para o BD
            string s_SQL_Comando = "INSERT INTO TB_ALUNO " +
                                   "( " +
                                   "I_COD_PESSOA, " +
                                   "S_MAT_ALUNO " +
                                   ") " +
                                   "VALUES " +
                                   "( " +
                                   "@I_COD_PESSOA, " +
                                   "@S_MAT_ALUNO " +
                                   "); " +
                                   " SELECT IDENT_CURRENT('TB_ALUNO') AS 'ID' ";

            // Objeto que executará a instrução SQL acima.
            SqlCommand obj_Cmd = new SqlCommand(s_SQL_Comando, obj_Con);

            // Passo os parametros dos dados dos atributos para a SQL
            obj_Cmd.Parameters.AddWithValue("@I_COD_PESSOA", pobj_Aluno.Cod_Pessoa);
            obj_Cmd.Parameters.AddWithValue("@S_MAT_ALUNO", pobj_Aluno.Mat_Aluno);

            try
            {
                // Abrir a Conexão
                obj_Con.Open();
                // Executar o comando de forma escalar
                int ID = Convert.ToInt16(obj_Cmd.ExecuteScalar());
                // Fechar a Conexão
                obj_Con.Close();
                return ID;

            }
            catch (Exception Erro)
            {
                MessageBox.Show(Erro.Message, "ERRO FATAL NA INCLUSÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

        }

        /***********************************************************************
        *        Método: Alterar
        *     Parametro: Objeto Aluno
        *          Obs.: Responsável por alterar um registro na tabela Aluno.
        *   Dt. Criação: 11/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public bool Alterar(Aluno pobj_Aluno)
        {
            // Criar o objeto de conexão com o banco.
            Connection obj_Con = new SqlConnection(Connection.ConectionPath());

            // A instrução de comando da SQL para o BD
            string s_SQL_Comando = "UPDATE TB_ALUNO SET " +
                                   "I_COD_PESSOA = @I_COD_PESSOA, " +
                                   "S_MAT_ALUNO  = @S_MAT_ALUNO " +
                                   "WHERE I_COD_ALUNO = @I_COD_ALUNO;";


            // Objeto que executará a instrução SQL acima.
            SqlCommand obj_Cmd = new SqlCommand(s_SQL_Comando, obj_Con);

            // Passo os parametros dos dados dos atributos para a SQL
            obj_Cmd.Parameters.AddWithValue("@I_COD_ALUNO", pobj_Aluno.Cod_Aluno);
            obj_Cmd.Parameters.AddWithValue("@I_COD_PESSOA", pobj_Aluno.Cod_Pessoa);
            obj_Cmd.Parameters.AddWithValue("@S_MAT_ALUNO", pobj_Aluno.Mat_Aluno);

            try
            {
                // Abrir a Conexão
                obj_Con.Open();
                // Executar o comando de forma escalar
                obj_Cmd.ExecuteNonQuery();
                // Fechar a Conexão
                obj_Con.Close();
                return true;

            }
            catch (Exception Erro)
            {
                MessageBox.Show(Erro.Message, "ERRO FATAL NA ALTERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        /***********************************************************************
        *        Método: Excluir
        *     Parametro: Objeto Aluno
        *          Obs.: Responsável por Excluir um registro na tabela Aluno.
        *   Dt. Criação: 11/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public bool Excluir(Aluno pobj_Aluno)
        {
            SqlConnection obj_Con = new SqlConnection(Connection.ConectionPath());
            string s_SQL_Comando = "DELETE FROM TB_ALUNO " +
                                   "WHERE I_COD_ALUNO = @I_COD_ALUNO;";
            SqlCommand obj_Cmd = new SqlCommand(s_SQL_Comando, obj_Con);
            obj_Cmd.Parameters.AddWithValue("@I_COD_ALUNO", pobj_Aluno.Cod_Aluno);
            try
            {
                obj_Con.Open();
                obj_Cmd.ExecuteNonQuery();
                obj_Con.Close();
                return true;
            }
            catch (Exception Erro)
            {
                MessageBox.Show(Erro.Message, "ERRO FATAL NA EXCLUSÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /***********************************************************************
        *        Método: FindByCodAluno
        *     Parametro: Objeto Aluno
        *          Obs.: Responsável por buscar um registro na tabela Aluno.
        *   Dt. Criação: 11/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public Aluno FindByCodAluno(Aluno pobj_Aluno)
        {
            // Criar o objeto de conexão com o banco.
            SqlConnection obj_Con = new SqlConnection(Connection.ConectionPath());

            // A instrução de comando da SQL para o BD
            string s_SQL_Comando = "SELECT * FROM TB_ALUNO " +
                                   "WHERE I_COD_ALUNO = @I_COD_ALUNO;";


            // Objeto que executará a instrução SQL acima.
            SqlCommand obj_Cmd = new SqlCommand(s_SQL_Comando, obj_Con);

            // Passo os parametros dos dados dos atributos para a SQL
            obj_Cmd.Parameters.AddWithValue("@I_COD_ALUNO", pobj_Aluno.Cod_Aluno);

            try
            {
                // Abrir a Conexão
                obj_Con.Open();

                // Cria o objeto de leitura
                SqlDataReader obj_Dtr = obj_Cmd.ExecuteReader();

                if (obj_Dtr.HasRows)
                {
                    obj_Dtr.Read();
                    pobj_Aluno.Cod_Aluno = Convert.ToInt16(obj_Dtr["I_COD_ALUNO"]);
                    pobj_Aluno.Cod_Pessoa = Convert.ToInt16(obj_Dtr["I_COD_PESSOA"]);
                    pobj_Aluno.Mat_Aluno = obj_Dtr["S_MAT_ALUNO"].ToString();
                    obj_Con.Close();
                    obj_Dtr.Close();
                    return pobj_Aluno;
                }
                else
                {
                    obj_Con.Close();
                    obj_Dtr.Close();
                    return null;
                }

            }
            catch (Exception Erro)
            {
                MessageBox.Show(Erro.Message, "ERRO FATAL NA BUSCA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        /***********************************************************************
        *        Método: FindAllAluno
        *          Obs.: Responsável por buscar todos os registros na tabela Aluno.
        *   Dt. Criação: 12/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public List<Aluno> FindAllAluno()
        {
            // Criar o objeto de conexão com o banco.
            SqlConnection obj_Con = new SqlConnection(Connection.ConectionPath());

            // A instrução de comando da SQL para o BD
            string s_SQL_Comando = "SELECT * FROM TB_ALUNO ";

            // Objeto que executará a instrução SQL acima.
            SqlCommand obj_Cmd = new SqlCommand(s_SQL_Comando, obj_Con);

            try
            {
                // Abrir a Conexão
                obj_Con.Open();

                // Cria o objeto de leitura
                SqlDataReader obj_Dtr = obj_Cmd.ExecuteReader();

                // Cria a Lista para receber os alunos da Tabela
                List<Aluno> Lista = new List<Aluno>();

                if (obj_Dtr.HasRows)
                {
                    // Enquanto tiver linha, faça.
                    while (obj_Dtr.Read())
                    {
                        Aluno obj_Aluno = new Aluno();
                        obj_Aluno.Cod_Aluno = Convert.ToInt16(obj_Dtr["I_COD_ALUNO"]);
                        obj_Aluno.Cod_Pessoa = Convert.ToInt16(obj_Dtr["I_COD_PESSOA"]);
                        obj_Aluno.Mat_Aluno = obj_Dtr["S_MAT_ALUNO"].ToString();
                        Lista.Add(obj_Aluno);
                    }
                    // Fecho a conexão com o BD
                    obj_Con.Close();

                    // Fecho o DataReader
                    obj_Dtr.Close();

                    return Lista;

                }
                else
                {
                    obj_Con.Close();
                    obj_Dtr.Close();
                    return null;
                }

            }
            catch (Exception Erro)
            {
                MessageBox.Show(Erro.Message, "ERRO FATAL NA BUSCA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
