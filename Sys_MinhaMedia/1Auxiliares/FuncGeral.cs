/***********************************************************************
 *          Nome: FuncGeral
 *          obs.: Representa a classe de Funções Gerais. 
 *                A Classe possui metodos públicos que serão utilizados 
 *                por Formulários e Classes
 *   Dt. Criação: 09/02/2023
 * Dt. Alteração: --
 *    Criada por: WeltonOliveira
 * *********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;


namespace SysSchool
{
    class FuncGeral
    {
        /// <SUMMARY>
        /// Vetor de byte utilizados para a criptografia (chave externa)
        /// </SUMMARY>
        private static byte[] bIV = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

        /// <summary>
        /// Representação de valor em base 64 (chave interna)
        /// O valor represanta a transformação para base64 de 
        /// um conjunto de 32 caracteres (8 * 32 - 256 bits)
        /// a chave é: "Criptografia com Rijndael / AES"
        /// </summary>
        private const string cryptoKey = "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";

        /***********************************************************************
        * NOME:            Criptografa        
        * METODO:          Criptografa o Password do usuário e retorna o 
        *                   Password criptografado
        * PARAMETRO:       String sPassWord
        * RETORNO:         String 
        * DT CRIAÇÃO:      09/02/2023    
        * DT ALTERAÇÃO:    -
        * ESCRITA POR:     Monstro (mFacine) 
        ***********************************************************************/
        public string Criptografa(string sPassWord)
        {
            try
            {
                // Se a string não está vazia, executa a criptografia
                if (!string.IsNullOrEmpty(sPassWord))
                {
                    // Cria instancias de vetores de bytes com as chaves                
                    byte[] bKey = Convert.FromBase64String(cryptoKey);
                    byte[] bText = new UTF8Encoding().GetBytes(sPassWord);

                    // Instancia a classe de criptografia Rijndael
                    Rijndael rijndael = new RijndaelManaged();

                    
                    // Define o tamanho da chave "256 = 8 * 32"                
                    // Lembre-se: chaves possíves:                
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                    rijndael.KeySize = 256;

                    
                    // Cria o espaço de memória para guardar o valor criptografado:                
                    MemoryStream mStream = new MemoryStream();
                    // Instancia o encriptador                 
                    CryptoStream encryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateEncryptor(bKey, bIV),
                        CryptoStreamMode.Write);

                    
                    // Faz a escrita dos dados criptografados no espaço de memória
                    encryptor.Write(bText, 0, bText.Length);
                    // Despeja toda a memória.                
                    encryptor.FlushFinalBlock();
                    // Pega o vetor de bytes da memória e gera a string criptografada                
                    return Convert.ToBase64String(mStream.ToArray());
                }
                else
                {
                    //Se a string for vazia retorna nulo                
                    return null;
                }
            }
            catch (Exception ex)
            {
                //Se algum erro ocorrer, dispara a exceção            
                throw new ApplicationException("Erro ao criptografar", ex);
            }
        }



        /*****************************************************************************
        * Nome           : DesCriptografa
        * Procedimento   : Descriptografa o password do usuário e retorna o 
        *                  pass descriptografado
        * Parametros     : sCriptoPassWord
        * Data  Criação  : 09/02/2022
        * Data Alteração : -
        * Escrito por    : WeltonOliveira
        * ***************************************************************************/
        public string DesCriptografa(string sCriptoPassWord)
        {
            try
            {
                //Se a string não está vazia, executa a criptografia           
                if (!string.IsNullOrEmpty(sCriptoPassWord))
                {
                    //Cria instancias de vetores de bytes com as chaves                
                    byte[] bKey = Convert.FromBase64String(cryptoKey);
                    byte[] bText = Convert.FromBase64String(sCriptoPassWord);

                    // Instancia a classe de criptografia Rijndael                
                    Rijndael rijndael = new RijndaelManaged();

                    
                    // Define o tamanho da chave "256 = 8 * 32"                
                    // Lembre-se: chaves possíves:                
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                    rijndael.KeySize = 256;

                    // Cria o espaço de memória para guardar o valor DEScriptografado:               
                    MemoryStream mStream = new MemoryStream();

                    // Instancia o Decriptador                 
                    CryptoStream decryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateDecryptor(bKey, bIV),
                        CryptoStreamMode.Write);

                   
                    // Faz a escrita dos dados criptografados no espaço de memória   
                    decryptor.Write(bText, 0, bText.Length);
                    // Despeja toda a memória.                
                    decryptor.FlushFinalBlock();
                    // Instancia a classe de codificação para que a string venha de forma correta         
                    UTF8Encoding utf8 = new UTF8Encoding();
                    // Com o vetor de bytes da memória, gera a string descritografada em UTF8       
                    return utf8.GetString(mStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo                
                    return null;
                }
            }
            catch (Exception ex)
            {
                //Se algum erro ocorrer, dispara a exceção            
                throw new ApplicationException("Erro ao descriptografar", ex);
            }
        }

        /***********************************************************************
        *        Método: VerificaNulos
        *     Parametro: Formulário Ativo
        *          Obs.: Responsável por Verificar se os componentes editáveis 
        *          de texto estão nulos no formulários ativo.
        *   Dt. Criação: 10/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public void VerificaNulos(Form pobj_Form)
        {
            foreach (Control pnl in pobj_Form.Controls)
            {
                if (pnl is Panel && pnl.Name == "pnl_Detail")
                {
                    foreach (Control ctrl in pnl.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            if (((TextBox)ctrl).Text == "")
                            {
                                MessageBox.Show("Um campo não foi preenchido.", "ENTRADA INVALIDA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ((TextBox)ctrl).Focus();
                                break;
                            }
                        }
                    }
                }
            }
        }

        /***********************************************************************
        *        Método: LimpaTela
        *     Parametro: Formulário Ativo
        *          Obs.: Responsável por Limpar os componentes editáveis dos 
        *                formulários.
        *   Dt. Criação: 10/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public void LimpaTela(Form pobj_Form)
        {
            foreach (Control pnl in pobj_Form.Controls)
            {
                if (pnl is Panel && pnl.Name == "pnl_Detail")
                {
                    foreach (Control ctrl in pnl.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            ((TextBox)ctrl).Clear();
                        }

                        if (ctrl is CheckBox)
                        {
                            ((CheckBox)ctrl).Checked = false;
                        }

                        if (ctrl is Label && Convert.ToInt16(ctrl.Tag) == 1)
                        {
                            ((Label)ctrl).Text = "";
                        }
                    }
                }
            }
        }


        /***********************************************************************
        *        Método: HabilitaTela
        *     Parametro: Formulário Ativo e Booleano (True ou False)
        *          Obs.: Responsável por Habilitar os componentes editáveis dos 
        *                formulários.
        *   Dt. Criação: 10/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public void HabilitaTela(Form pobj_Form, bool b_Hab)
        {
            foreach (Control pnl in pobj_Form.Controls)
            {
                if (pnl is Panel && pnl.Name == "pnl_Detail")
                {
                    foreach (Control ctrl in pnl.Controls)
                    {
                        if (ctrl is TextBox && Convert.ToInt16(ctrl.Tag) != 1)
                        {
                            ((TextBox)ctrl).Enabled = b_Hab;
                        }

                        if (ctrl is CheckBox)
                        {
                            ((CheckBox)ctrl).Enabled = b_Hab;
                        }

                        if (ctrl is Button)
                        {
                            ((Button)ctrl).Enabled = b_Hab;
                        }

                        if (ctrl is ListView)
                        {
                            ((ListView)ctrl).Enabled = b_Hab;
                        }

                    }
                }
            }
        }

        /***********************************************************************
        *        Método: StatusBtn
        *     Parametro: Formulário Ativo e uma variável int
        *          Obs.: Responsável por trazer o Status dos botões na tela dos 
        *                formulários.
        *                Status (1, 2 ou 3)
        *                caso 1 -> btn_Novo será true e os demais false. 
        *                caso 2 -> os três primeiros serão true e os demais false.
        *                caso 3 -> os três primeiros serão false e os demais true.
        *                
        *   Dt. Criação: 13/02/2023
        * Dt. Alteração: --
        *    Criada por: WeltonOliveira
        ***********************************************************************/
        public void StatusBtn(Form pobj_Form, int pi_Status)
        {
            foreach (Control pnl in pobj_Form.Controls)
            {
                if (pnl is Panel && pnl.Name == "pnl_Button")
                {
                    foreach (Control ctrl in pnl.Controls)
                    {
                        switch (pi_Status)
                        {
                            case 1:
                                {
                                    if (ctrl.Name == "btn_Novo")
                                    {
                                        ctrl.Enabled = true;
                                    }

                                    if (ctrl.Name == "btn_Alterar")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    if (ctrl.Name == "btn_Excluir")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    if (ctrl.Name == "btn_Confirmar")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    if (ctrl.Name == "btn_Cancelar")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    break;
                                }

                            case 2:
                                {
                                    if (ctrl.Name == "btn_Novo")
                                    {
                                        ctrl.Enabled = true;
                                    }

                                    if (ctrl.Name == "btn_Alterar")
                                    {
                                        ctrl.Enabled = true;
                                    }

                                    if (ctrl.Name == "btn_Excluir")
                                    {
                                        ctrl.Enabled = true;
                                    }

                                    if (ctrl.Name == "btn_Confirmar")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    if (ctrl.Name == "btn_Cancelar")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    break;
                                }

                            case 3:
                                {

                                    if (ctrl.Name == "btn_Novo")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    if (ctrl.Name == "btn_Alterar")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    if (ctrl.Name == "btn_Excluir")
                                    {
                                        ctrl.Enabled = false;
                                    }

                                    if (ctrl.Name == "btn_Confirmar")
                                    {
                                        ctrl.Enabled = true;
                                    }

                                    if (ctrl.Name == "btn_Cancelar")
                                    {
                                        ctrl.Enabled = true;
                                    }

                                    break;
                                }
                        }
                    }
                }
            }
        }


    }
}

