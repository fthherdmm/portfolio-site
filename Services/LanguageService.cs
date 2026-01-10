using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace PortfolioSite.Services
{
    public class LanguageService
    {
        private readonly IJSRuntime _js;

        // Constructor'da JS Runtime'ı alıyoruz
        public LanguageService(IJSRuntime js)
        {
            _js = js;
        }

        public event Action? OnChange;
        public string CurrentLanguage { get; private set; } = "TR";

        // 1. Dili Ayarla ve Kaydet (Async yaptık)
        public async Task SetLanguage(string lang)
        {
            CurrentLanguage = lang;
            // Tarayıcı hafızasına yaz
            await _js.InvokeVoidAsync("localStorage.setItem", "app_lang", lang);
            OnChange?.Invoke();
        }

        // 2. Uygulama Açılınca Dili Yükle
        public async Task InitAsync()
        {
            // Tarayıcı hafızasından oku
            var savedLang = await _js.InvokeAsync<string>("localStorage.getItem", "app_lang");
            
            if (!string.IsNullOrEmpty(savedLang))
            {
                CurrentLanguage = savedLang;
                OnChange?.Invoke(); // Sayfaları uyar
            }
        }

        public string Get(string key)
        {
            if (_translations.ContainsKey(key) && _translations[key].ContainsKey(CurrentLanguage))
            {
                return _translations[key][CurrentLanguage];
            }
            return key;
        }

        // --- SÖZLÜK ---
        private readonly Dictionary<string, Dictionary<string, string>> _translations = new()
        {
            // NAVIGASYON LİNKLERİ
            { "Nav_Projects", new() { { "TR", "Projeler" }, { "EN", "Projects" } } },
            { "Nav_DevLog", new() { { "TR", "DevLog" }, { "EN", "DevLog" } } },
            { "Nav_Timeline", new() { { "TR", "Zaman Tüneli" }, { "EN", "Timeline" } } },
            { "Nav_Profile", new() { { "TR", "Profil" }, { "EN", "Profile" } } },
            { "Nav_Contact", new() { { "TR", "İletişim" }, { "EN", "Contact" } } },
            { "Nav_Private", new() { { "TR", "Gizli" }, { "EN", "Private" } } }, // Private badge için
            
            // İLETİŞİM SAYFASI
            { "Contact_Title", new() { { "TR", "Bana Ulaşın 📬" }, { "EN", "Get in Touch 📬" } } },
            { "Contact_Desc", new() { { "TR", "Projeleriniz için iş birliği yapmak veya sadece merhaba demek isterseniz form doldurabilirsiniz." }, { "EN", "Whether you have a question or just want to say hi, feel free to fill out the form." } } },
            { "Contact_Channels", new() { { "TR", "İletişim Kanalları" }, { "EN", "Contact Channels" } } },
            { "Contact_Location", new() { { "TR", "Şu an Ankara, Türkiye konumundayım." }, { "EN", "Currently based in Ankara, Turkiye." } } },
            { "Form_Header", new() { { "TR", "Mesaj Gönder" }, { "EN", "Send Message" } } },
            { "Form_Name", new() { { "TR", "ADINIZ SOYADINIZ" }, { "EN", "FULL NAME" } } },
            { "Form_Email", new() { { "TR", "E-POSTA ADRESİNİZ" }, { "EN", "EMAIL ADDRESS" } } },
            { "Form_Message", new() { { "TR", "MESAJINIZ" }, { "EN", "YOUR MESSAGE" } } },
            { "Form_Btn_Send", new() { { "TR", "🚀 Gönder" }, { "EN", "🚀 Send" } } },
            { "Form_Btn_Sending", new() { { "TR", "📨 Gönderiliyor..." }, { "EN", "📨 Sending..." } } },
            { "Msg_Success_Title", new() { { "TR", "Mesajınız İletildi!" }, { "EN", "Message Sent!" } } },
            { "Msg_Success_Desc", new() { { "TR", "En kısa sürede dönüş yapacağım." }, { "EN", "I will get back to you soon." } } },
            { "Msg_New_Btn", new() { { "TR", "Yeni Mesaj Gönder" }, { "EN", "Send New Message" } } },
            { "Err_Required", new() { { "TR", "Lütfen tüm alanları doldurun." }, { "EN", "Please fill in all fields." } } },
            
            // ANA SAYFA (HOME)
            { "Home_Title", new() { { "TR", "Ana Sayfa" }, { "EN", "Home" } } },
            { "Hero_Badge", new() { { "TR", "👋 Merhaba, Ben Muhammed Fatih Erdem" }, { "EN", "👋 Hello, I'm Muhammed Fatih Erdem" } } },
            { "Hero_Title1", new() { { "TR", "Bilgisayar Mühendisi &" }, { "EN", "Computer Engineer &" } } },
            { "Hero_Title2", new() { { "TR", "Backend Geliştirici." }, { "EN", "Backend Developer." } } },
            { "Hero_Desc", new() { { "TR", "C# ve .NET ekosistemi üzerine uzmanlaşarak ölçeklenebilir ve sürdürülebilir yazılım mimarileri tasarlıyorum. Bu platform; geliştirdiğim açık kaynak projeleri ve teknik gelişim sürecimi şeffaf bir şekilde belgelediğim dijital çalışma alanımdır." }, { "EN", "I design scalable and maintainable software architectures specializing in C# and the .NET ecosystem. This platform is my digital workspace where I transparently document my open-source projects and technical growth journey." } } },
            { "Btn_Profile", new() { { "TR", "Profilimi İncele" }, { "EN", "View Profile" } } },
            { "Btn_Connect", new() { { "TR", "Bağlantı Kur" }, { "EN", "Connect With Me" } } },
            // BÖLÜM BAŞLIKLARI
            { "Sec_RecentProjects", new() { { "TR", "🔥 Son Çalışmalar" }, { "EN", "🔥 Recent Works" } } },
            { "Sec_RecentLogs", new() { { "TR", "📓 Son Günlükler" }, { "EN", "📓 Recent Logs" } } },
            // BUTONLAR VE UYARILAR
            { "Btn_SeeAll", new() { { "TR", "Tümünü Gör ➔" }, { "EN", "See All ➔" } } },
            { "Badge_Project", new() { { "TR", "PROJE" }, { "EN", "PROJECT" } } },
            { "Msg_NoDesc", new() { { "TR", "Açıklama yok." }, { "EN", "No description." } } },
            { "Btn_Inspect", new() { { "TR", "İncele ➔" }, { "EN", "Inspect ➔" } } },
            { "Msg_NoLogs", new() { { "TR", "Henüz günlük girişi yok." }, { "EN", "No logs yet." } } },
            { "Btn_ReadMore", new() { { "TR", "Devamını Oku ➔" }, { "EN", "Read More ➔" } } },
            
            // PROJELER SAYFASI (PROJECTS)
            { "Projects_PageTitle", new() { { "TR", "Tüm Projeler" }, { "EN", "All Projects" } } },
            { "Projects_Header", new() { { "TR", "Tüm Projeler 🚀" }, { "EN", "All Projects 🚀" } } },
            { "Projects_Desc", new() { { "TR", "Geçmişten bugüne üzerinde çalıştığım tüm açık kaynak projeler." }, { "EN", "All open source projects I have worked on from past to present." } } },
            { "Link_GitHub", new() { { "TR", "GitHub" }, { "EN", "GitHub" } } },
            
            // DEVLOG SAYFASI
            { "DevLog_PageTitle", new() { { "TR", "DevLog | Günlük" }, { "EN", "DevLog | Journal" } } },
            { "DevLog_Desc", new() { { "TR", "Yazılım serüvenimden notlar. Bugün neler kodladım, neler öğrendim?" }, { "EN", "Notes from my software journey. What did I code and learn today?" } } },
            { "Stat_Commit", new() { { "TR", "Commit" }, { "EN", "Commit" } } },
            { "Stat_NoCommit", new() { { "TR", "Bugün kod commitlenmedi" }, { "EN", "No code committed today" } } },
            
            // ZAMAN TÜNELİ (TIMELINE)
            { "Timeline_PageTitle", new() { { "TR", "Zaman Tüneli" }, { "EN", "Timeline" } } },
            { "Timeline_Header", new() { { "TR", "Geliştirme Günlüğü" }, { "EN", "Development Journal" } } },
            { "Timeline_Desc", new() { { "TR", "Projelerime eklediğim son özellikler ve güncellemeler." }, { "EN", "Latest features and updates added to my projects." } } },
            { "Timeline_Loading", new() { { "TR", "Geçmiş yükleniyor..." }, { "EN", "Loading history..." } } },
            { "Timeline_Unknown", new() { { "TR", "Bilinmeyen Proje" }, { "EN", "Unknown Project" } } },
            { "Timeline_Detail", new() { { "TR", "Detay ➔" }, { "EN", "Details ➔" } } },
            { "Timeline_Note", new() { { "TR", "✏️ Notum" }, { "EN", "✏️ My Note" } } },
            
            // --- PROFİL SAYFASI ---
            { "Profile_PageTitle", new() { { "TR", "Profilim" }, { "EN", "My Profile" } } },
            { "Profile_JobTitle", new() { { "TR", "Yazılım Mühendisi | .NET Backend Developer" }, { "EN", "Software Engineer | .NET Backend Developer" } } },
            // HAKKIMDA KISMI
            { "Sec_About", new() { { "TR", "👨‍💻 Hakkımda" }, { "EN", "👨‍💻 About Me" } } },
            { "Bio_P1", new() { { "TR", "C# ve Nesne Yönelimli Programlama (OOP) temelleri güçlü bir <strong>Bilgisayar Mühendisliği</strong> mezunuyum. Özellikle <strong>.NET Core (8/9)</strong> ekosistemi ile ölçeklenebilir ve yüksek performanslı backend sistemleri geliştirme konusunda tutkuluyum." }, { "EN", "I am a <strong>Computer Engineering</strong> graduate with a strong foundation in C# and Object-Oriented Programming (OOP). I am passionate about building scalable and high-performance backend systems, especially using the <strong>.NET Core (8/9)</strong> ecosystem." } } },
            { "Bio_P2", new() { { "TR", "<strong>Clean Architecture</strong>, <strong>DDD (Domain-Driven Design)</strong> ve <strong>Mikroservis</strong> prensipleri konusunda her gün daha iyisi olma yolunda kendimi geliştiriyorum. Gerçek dünya problemlerini çözerken SOLID prensiplerini ve Tasarım Desenlerini (Design Patterns) etkin bir şekilde kullanıyorum." }, { "EN", "I am constantly improving myself in <strong>Clean Architecture</strong>, <strong>DDD (Domain-Driven Design)</strong>, and <strong>Microservices</strong> principles. I effectively apply SOLID principles and Design Patterns when solving real-world problems." } } },
            { "Bio_P3", new() { { "TR", "Günümüzde kod yazmaktan ve 'syntax' bilmekten daha önemlisi temiz bir mimari tasarlayıp yüksek performanslı bir sistem oluşturabilmek olduğunu düşünüyorum. Bunun için bu tür konularda kendimi geliştirmeye özen gösteriyorum." }, { "EN", "I believe that designing a clean architecture and creating a high-performance system is more important than just knowing syntax and writing code. Therefore, I take care to develop myself in these areas." } } },
            { "Bio_P4", new() { { "TR", "Şu anda ASP.NET Core, PostgreSQL ve Docker yetkinliklerimi kullanarak değer yaratabileceğim bir <strong>Backend Developer</strong> rolü arıyorum." }, { "EN", "I am currently seeking a <strong>Backend Developer</strong> role where I can create value using my skills in ASP.NET Core, PostgreSQL, and Docker." } } },
            // EĞİTİM KISMI
            { "Sec_Education", new() { { "TR", "🎓 Eğitim" }, { "EN", "🎓 Education" } } },
            { "Edu_YBU_Name", new() { { "TR", "Yıldırım Beyazıt Üniversitesi" }, { "EN", "Yildirim Beyazit University" } } },
            { "Edu_YBU_Dept", new() { { "TR", "Bilgisayar Mühendisliği (İngilizce) | 2020 - 2025" }, { "EN", "Computer Engineering (English) | 2020 - 2025" } } },
            { "Edu_YBU_Desc", new() { { "TR", "GPA: 3.02. Mühendislik temelleri ve yazılım mimarileri üzerinde kendimi geliştirdim." }, { "EN", "GPA: 3.02. Developed myself on engineering fundamentals and software architectures." } } },
            // DENEYİM KISMI
            { "Sec_Experience", new() { { "TR", "🚀 Deneyim" }, { "EN", "🚀 Experience" } } },
            // TUSAŞ
            { "Exp_Tusas_Title", new() { { "TR", "TUSAŞ (Turkish Aerospace Industries)" }, { "EN", "TUSAS (Turkish Aerospace Industries)" } } },
            { "Exp_Tusas_Role", new() { { "TR", "Yazılım Geliştirici (Kurum İçi Girişimcilik) | Ocak 2025 - Eylül 2025" }, { "EN", "Software Developer (Internal Entrepreneurship) | Jan 2025 - Sep 2025" } } },
            { "Exp_Tusas_Desc", new() { { "TR", "Resmi olarak kabul edilen bir kurum içi girişimcilik projesinin uçtan uca geliştirme sürecini yönettim. Modüler ve sürdürülebilir kod yapıları tasarladım." }, { "EN", "Led the end-to-end development lifecycle of an officially accepted internal startup project. Designed modular and sustainable code structures." } } },
            // ESC
            { "Exp_ESC_Title", new() { { "TR", "European Solidarity Corps (ESC)" }, { "EN", "European Solidarity Corps (ESC)" } } },
            { "Exp_ESC_Role", new() { { "TR", "Proje Koordinatörü | Macaristan | Eyl 2025 - Ara 2025" }, { "EN", "Project Coordinator | Hungary | Sep 2025 - Dec 2025" } } },
            { "Exp_ESC_Desc", new() { { "TR", "Uluslararası çevik takımlarda liderlik ve kültürlerarası iletişim yetkinliklerimi geliştirdim." }, { "EN", "Improved leadership and cross-cultural communication skills in international agile teams." } } },
            // Simge
            { "Exp_Simge_Title", new() { { "TR", "Simge Simülasyon" }, { "EN", "Simge Simulation" } } },
            { "Exp_Simge_Role", new() { { "TR", "Yazılım Mühendisi Stajyeri (Part-Time) | 2024 - 2025" }, { "EN", "Software Engineer Intern (Part-Time) | 2024 - 2025" } } },
            { "Exp_Simge_Desc", new() { { "TR", "Unity Engine ile yüksek performanslı C# kodlama. Burada tasarım desenleri ve Clean architecture açısından kendimi profesyonel anlamda çok geliştirdim. Bana çok şey kattığını düşünüyorum." }, { "EN", "High-performance C# coding with Unity Engine. I developed myself professionally in terms of design patterns and Clean Architecture here. It contributed significantly to my growth." } } },
            // Udo
            { "Exp_Udo_Title", new() { { "TR", "Udo Games" }, { "EN", "Udo Games" } } },
            { "Exp_Udo_Role", new() { { "TR", "Stajyer | 2023" }, { "EN", "Intern | 2023" } } },
            { "Exp_Udo_Desc", new() { { "TR", "C# programlama mantığı, algoritma geliştirme ve debugging üzerine pratik deneyim." }, { "EN", "Practical experience in C# programming logic, algorithm development, and debugging." } } },
            // YETKİNLİKLER KISMI
            { "Sec_Skills", new() { { "TR", "🛠️ Teknik Yetkinlikler" }, { "EN", "🛠️ Technical Skills" } } },
            { "Skill_Cat_Lang", new() { { "TR", "Programlama Dilleri" }, { "EN", "Programming Languages" } } },
            { "Skill_Cat_Framework", new() { { "TR", "Frameworks & Kütüphaneler" }, { "EN", "Frameworks & Libraries" } } },
            { "Skill_Cat_Db", new() { { "TR", "Veritabanı & Depolama" }, { "EN", "Database & Storage" } } },
            { "Skill_Cat_Arch", new() { { "TR", "Mimari & Prensipler" }, { "EN", "Architecture & Principles" } } },
            { "Skill_Cat_DevOps", new() { { "TR", "DevOps & Araçlar" }, { "EN", "DevOps & Tools" } } },
            // FOOTER
            { "Profile_Contact_Text", new() { { "TR", "Projeler veya iş fırsatları hakkında konuşmak için:" }, { "EN", "To discuss projects or job opportunities:" } } },
            { "Btn_SendEmail", new() { { "TR", "📧 E-Posta Gönder" }, { "EN", "📧 Send Email" } } },
            
            // PROJE DETAY SAYFASI
            { "ProjDet_Title", new() { { "TR", "Detaylar" }, { "EN", "Details" } } },
            { "ProjDet_NotFound_Title", new() { { "TR", "😔 Proje Bulunamadı" }, { "EN", "😔 Project Not Found" } } },
            { "ProjDet_NotFound_Desc", new() { { "TR", "Aradığınız proje veritabanında mevcut değil veya silinmiş." }, { "EN", "The project you are looking for does not exist or has been deleted." } } },
            { "Btn_BackHome", new() { { "TR", "Ana Sayfaya Dön" }, { "EN", "Return Home" } } },
            { "Btn_Back", new() { { "TR", "← Geri Dön" }, { "EN", "← Go Back" } } },
            { "ProjDet_Story_Title", new() { { "TR", "Proje Hikayesi" }, { "EN", "Project Story" } } },
            { "ProjDet_NoStory", new() { { "TR", "Henüz bir hikaye girilmemiş..." }, { "EN", "No story entered yet..." } } },
            { "ProjDet_DevLog_Title", new() { { "TR", "Geliştirme Günlüğü" }, { "EN", "Development Log" } } },
            { "ProjDet_NoCommits", new() { { "TR", "Bu projeye ait commit bulunamadı." }, { "EN", "No commits found for this project." } } },
        };
    }
}