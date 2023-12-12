Kumpulan hasil compile dan build yang siap di deploy di iis
Tidak perlu compile di visual studio
Tinggal copy paste ke folder www sesuaikan folder aplikasinya

- web_phe_actris
di deploy di server dengan mode SSO (default)

-localhost_phe_actris
Untuk di deploy di server iis local
Dengan path localhost/PHE_ACTRIS

-localhost_phe_actris_bypasslogin
Untuk di deploy di server iis local
Dengan path localhost/PHE_ACTRIS_bypasslogin
Berguna untuk login sebagai user lain, tanpa masukin password

-background-service
Untuk dijalankan di VM Server yang akan running scheduler
Kenapa dipisah dari web apps karena web apps ketika tidak ada request masuk ke sleep mode
Bisa saja di gabungin di web apps, dengan membuat web apps tidak bisa masuk ke sleep mode, tapi ada kekurangan akan consume resource (RAM & CPU)
