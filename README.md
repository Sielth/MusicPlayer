ReadMe MusicPlayer
==================

MusicPlayer repository

<https://github.com/Sielth/MusicPlayer>

Her ligger der source koden til projektet.

MusicRecommendationML

<https://github.com/Sielth/MusicRecommendationML/blob/main/mood_df.ipynb>

Her ligger der Colab Notebook fil, som indeholder det machine learning, jeg har udviklet, til at forudsige musikkens humør.

Krav
----

Til at køre applikationen skal man

-   Installer Docker Desktop (husk at aktivere Kubernetes i indstillinger)

-   Opdater host filen på maskinen

-   Åben Notepad som administrator

-   Tryk på File, Open, og slå denne sti op\
     C:\Windows\System32\drivers\etc\
    (husk at vælge All Files på nederste højre hjørnet)

-   Find den fil, der hedder host og åben den

-   Tilføj\
     127.0.0.1 acme.com\
    i slutning af filen og gem

Sæt up RavenDB
==============

-   Åben PoweShell og eksekver\
     docker run -d -p 8080:8080 -p 38888:38888 ravendb/ravendb

-   I browseren, gå til\
     localhost:8080\
    og sæt op RavenDB

-   Accepter EULA

-   Vælg UNSECURE

-   I Unsecure Mode Setup, udfyld IP feltet med\
     0.0.0.0\
    and check out the yellow box

-   Genstart Serveren

-   I RavenDB Studio (localhost:8080), opret to nye databases

-   En der hedder Track

-   En der hedder Playlist 

-   OBS! Være opmærksom på, at RavenDB Container skal lytte på enten 0.0.0.0 eller 172.17.0.2. Hvis den ikke virker, tjek på containerens Logs, og genstart containeren.

![](https://lh4.googleusercontent.com/SvEcz6X9LWBuVjAxHWps0_cZhnFTwCmE07m0gwn8dX-ehaFJNKTvb3mvME6i-TxJ7pQgU5e-AjKL8QCE0lBHVwU-gh-TJjt3YQ-TbeS0AfJ9Pmv9Oz6JLAgG1mJjk2aJ_oX1_OKV5HF8V9CGoQ)

Sæt up Music Player
===================

-   Clone MusicPlayer repository

-   Åben Power Shell i\
     MusicPlayer\src\K8S\K8S

-   Udfør disse kommandoer

-   kubectl apply -f tracks-depl.yaml

-   kubectl apply -f playlists-depl.yaml

-   kubectl apply -f moods-depl.yaml

-   kubectl apply -f tracks-np-srv.yaml

-   kubectl apply -f rabbitmq-depl.yaml

-   kubectl apply -f ingress-srv.yaml

-   kubectl kubectl apply -f <https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.2.0/deploy/static/provider/cloud/deploy.yaml>

Start Blazor App
=========================================================================================================================================================================================================

-   Åben PowerShell i\
     MusicPlayer\src\SPA\MusicPlayer.Blazor

-   Eksekver\
     dotnet run

-   Tilgå applikationen på browseren via\
     https://localhost:7001 
     
![](https://lh6.googleusercontent.com/rUSKZ8zsvBUhxZ_F7AWNpNg7_OWRKf6SQJI0-sSRdjYid5JsuCc9nd6z87qr-dzmE7XV8KjPNdKTaESBh8fTorpZaoL4qLnoMYypkInnjUEIf6DiIIGdSLx_TIUa0fqhqVb_CUOzgLPY9qOibw)
