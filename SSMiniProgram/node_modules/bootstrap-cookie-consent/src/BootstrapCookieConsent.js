/**
 * @version 0.0.1
 * @author Robin D https://www.robin-d.fr/
 * @license The MIT License (MIT)
 */
import CookieConsentApi from '../node_modules/cookie-consent-api/src/index.js';

var bsn = require("bootstrap.native");

class BootstrapCookieConsent
{

    constructor(conf = {})
    {
        const defaultConf = {
            'show_selector'   : '.cc',
            'accept_id'       : 'accept-cookie',
            'banner_text'     : 'Ce site utilise des services tiers susceptible de vous déposer un cookie. Pour une navigation optimale, acceptez-vous de les utiliser sur ce site ?',
            'button_text'     : 'J\'accepte',
            'banner_id'       : 'cookies-banner',
            'link_more_info'  : '#',
            'more_info_label' : 'En savoir plus',
            'details_title'   : 'Vie Privée',
            'details_text'    : 'Vous pouvez accepter ou refuster l\'utilisation sur ce site de certains services.',
            'checkbox_class'  : 'switch-sm',
            'method'          : 1, // 0: native boostrap, 1:jquery bootstrap or (string) 'bsn'
            services: [],
            services_descr: {}
        };


        this._conf = Object.assign({}, defaultConf, conf);

        this.cookieConsent = new CookieConsentApi(this._conf);

        if (this.cookieConsent.isAllConfigured() == false) {
            this._showBanner();
        }

        this.cookieConsent.on('allConfigured', ()=>{
            this._hideBanner();
        });

        document.querySelectorAll(this._conf.show_selector).forEach(
        (item)=>{
            item.addEventListener('click', ()=>{
                if (document.getElementById('cookie-modal') === null) {
                    this._createDetails();
                }

                if (this._conf.method === 1) {
                    $('#cookie-modal').modal('toggle');
                } else {
                    let modal = document.getElementById('cookie-modal');
                    let iModal = typeof this._conf.method == 'string' ? new eval(this._conf.method + '.Modal(modal)') : new Modal(modal);
                    iModal.show();
                }
            });
        });

    }

    _hideBanner() {
        let bannerElement = document.getElementById(this._conf.banner_id);
        if (bannerElement !== null) {
            bannerElement.parentNode.removeChild(bannerElement);
        }
    }

    _showBanner() {
        let parser = new DOMParser();

        let banner = document.createElement('div');
        banner.setAttribute('id', this._conf.banner_id);
        banner.setAttribute('class', 'alert alert-warning text-center');
        banner.innerHTML = this._conf.banner_text+' <button class="btn btn-success btn-gradient btn-sm" id="'+this._conf.accept_id+'">'+this._conf.button_text+'</button> <a href="'+this._conf.link_more_info+'">'+this._conf.more_info_label+'</a>';
        document.body.insertBefore(banner, document.body.firstChild);

        document.getElementById(this._conf.accept_id).addEventListener('click', ()=>{
            this.cookieConsent.acceptAll();
            this._hideBanner();
        });
    }

    _createDetails() {
        let modal = '';
	    modal += '<div class="modal-dialog" role="document"><div class="modal-content"><div class="modal-header">';
	    modal += '<h5 class="modal-title">' + this._conf.details_title + '</h5>';
	    modal += '<button type="button" class="close" data-dismiss="modal" aria-label="Fermer"><span aria-hidden="true">&times;</span></button></div>';
	    modal += '<div class="modal-body">';
	    modal += '<p>' + this._conf.details_text + ' <a href="' + this._conf.link_more_info + '">' + this._conf.more_info_label + '</a></p>';
	    modal += '<table class="table"><thead class="thead-light"><tr><th scope="col" class="col-10">Service</th><th scope="col" class="col-2">Accepter</th></tr></thead>';
	    modal += '<tbody>';

	    this._conf.services.forEach(elem => {
	      modal += '<tr><td>' + elem + (elem in this._conf.services_descr ? '<br><small>' + this._conf.services_descr[elem] + '</small>' : '') + '</td><td class="text-center">';
	      modal += '<span class="switch"><input type="checkbox" class="' + this._conf.checkbox_class + '" id="switch-' + elem + '"';
	      modal += this.cookieConsent.isAccepted(elem) ? ' checked' : '';
	      modal += '><label for="switch-' + elem + '"></label></span>';
	      modal += '</td></tr>';
	    });

	    modal += '</tbody></table></div></div></div6';

        let modalElement = document.createElement('div');
        modalElement.setAttribute('id', 'cookie-modal');
        modalElement.setAttribute('class', 'modal fade');
        modalElement.innerHTML = modal;

	    document.body.appendChild(modalElement);

        this._conf.services.forEach((elem)=>{
            document.getElementById('switch-'+ elem).addEventListener('change', ()=>{
                if (document.getElementById('switch-'+ elem).checked) {
                    this.cookieConsent.accept(elem);
                } else {
                    this.cookieConsent.refuse(elem);
                }
            });
        });
    }


}

export default BootstrapCookieConsent;
