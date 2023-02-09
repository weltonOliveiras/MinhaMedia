/***********************************************************************
 *          Nome: Connection
 *          obs.: Representa a classe de Conexão dom o Banco de Dados. 
 *   Dt. Criação: 09/02/2023
 * Dt. Alteração: --
 *    Criada por: WeltonOliveira
 * *********************************************************************/
namespace SysSchool
{
    public class Connection
    {
        //(09/02/2022 - WeltonOliveira) Metodo da classe que retorna o caminho do BD.
        public static string ConectionPath()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\welton.osilva2\OneDrive - SENAC - SP\Documentos\Projeto_MinhaMedia\Sys_MinhaMedia\BD_MinhaMedia\BD_MinhaMedia.mdf;Integrated Security = True; Connect Timeout = 15";
        }
    }
}

