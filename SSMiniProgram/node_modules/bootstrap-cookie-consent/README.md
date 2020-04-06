# Be compliant to GDPR (rgpd) like CNIL ask it

## Table of contents
* [Introduction](#introduction)
* [Installation](#installation)
    * [npm](https://www.npmjs.com/package/bootstrap-cookie-consent)
* [Usage](#usage)
* [Documentation](#documentation)
* [License](#license)
* [Contributors](#contributors)

## Introduction

This small bootstrap/jquery plugin permits to be compliant to GDPR (aka rgpd) like CNIL (France) ask.
Extend [`atillay/cookie-consent-api`](https://github.com/atillay/cookie-consent-api).

## Installation

In one command line :
```bash
npm install bootstrap-cookie-consent
```

## Usage

See `dist/demo.html` :

```html
<!-- Youtube player example -->
<div data-cookie-consent="YouTube">
    <img src="https://img.youtube.com/vi/R4lZyXjGLRs/0.jpg" width="560" height="315">
    <!--if-consent
        <iframe src="https://www.youtube.com/embed/R4lZyXjGLRs" width="560" height="315" style="background:#000;border:0;"></iframe>
    endif-->
</div>

<script>
document.addEventListener('DOMContentLoaded', function()
{
    new BootstrapCookieConsent({
        services: ['StatistiquesAnonymes', 'YouTube']
        services_descr: [
            'StatistiquesAnonymes' : 'Nous permet d\'améliorer le site en fonction de son utilisation',
            'YouTube': 'Affiche les vidéos du service youtube.com'
        ]
    });
});
</script>
```

## Documentation

To custom it :

```js
new BootstrapCookieConsent({

    services: [],

    services_descr: [],
    'show_selector'   : '.cc',
    'accept_id'       : 'accept-cookie',
    'banner_text'     : 'Ce site utilise des services tiers susceptible de vous déposer un cookie. Pour une navigation optimale, acceptez-vous de les utiliser sur ce site ?',
    'button_text'     : 'J\'accepte',
    'banner_id'       : 'cookies-banner',
    'link_more_info'  : '#',
    'more_info_label' : 'En savoir plus',
    'details_title'   : 'Vie Privée',
    'details_text'    : 'Vous pouvez accepter ou refuster l\'utilisation sur ce site de certains services.',
    'checkbox_class'  : 'custom-control-input',
    cookieName: 'cookie_consent_settings', // From cookie-consent-api
    cookieDuration: 365,
    cookieDomain: null,
    method: 1, // 0: native bootstrap, 1: jquery (classic) bootstrap
});
```

Other commands :

```bash
#To make the demo work
npm run-script start
#To Build the file
npm run-script build
```

## License

MIT (see the `LICENSE` file for details)

## Contributors

* Original author : [from the Alps Mountain](https://www.robin-d.fr/).
* ...

