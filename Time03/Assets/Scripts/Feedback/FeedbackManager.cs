using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour
{
    public GameObject FeedbackCanvas;
    public InputField FeedbackText;

    

    public void OpenFeedbackForm() {
        FeedbackCanvas.SetActive(true);
        FeedbackText.text = "";
    }

    public void SendFeedback() {
        StartCoroutine(SendMail());
        CloseFeedbackForm();
    }

    private IEnumerator SendMail() {
        yield return new WaitForSeconds(0);
        MailMessage mail = new MailMessage();
        Attachment data = new Attachment(SaveSystem.FeedbackPath);
        mail.From = new MailAddress("projetoalfamail@gmail.com");
        mail.To.Add("projetoalfamail@gmail.com");
        mail.Subject = ("Feedback");
        mail.Body = FeedbackText.text;
        mail.Attachments.Add(data);
        

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("projetoalfamail@gmail.com","@ProjetoAlfa03") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = 
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {return true;};
        smtpServer.Send(mail);
        Debug.Log("Enviado com Sucesso!");
    }

    private void CloseFeedbackForm() {
        FeedbackCanvas.SetActive(false);
    }
}
