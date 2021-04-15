﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SendGrid.Permissions
{
    partial class SendGridPermissionsBuilder
    {
        private readonly Dictionary<SendGridPermission, string[]> allPermissions = new Dictionary<SendGridPermission, string[]>
        {
            #region Admin
            { SendGridPermission.Admin, new []
            {
                "access_settings.activity.read",
                "access_settings.whitelist.create",
                "access_settings.whitelist.delete",
                "access_settings.whitelist.read",
                "access_settings.whitelist.update",
                "alerts.create",
                "alerts.delete",
                "alerts.read",
                "alerts.update",
                "api_keys.create",
                "api_keys.delete",
                "api_keys.read",
                "api_keys.update",
                "asm.groups.create",
                "asm.groups.delete",
                "asm.groups.read",
                "asm.groups.update",
                "billing.create",
                "billing.delete",
                "billing.read",
                "billing.update",
                "browsers.stats.read",
                "categories.create",
                "categories.delete",
                "categories.read",
                "categories.stats.read",
                "categories.stats.sums.read",
                "categories.update",
                "clients.desktop.stats.read",
                "clients.phone.stats.read",
                "clients.stats.read",
                "clients.tablet.stats.read",
                "clients.webmail.stats.read",
                "devices.stats.read",
                "email_activity.read",
                "geo.stats.read",
                "ips.assigned.read",
                "ips.pools.create",
                "ips.pools.delete",
                "ips.pools.ips.create",
                "ips.pools.ips.delete",
                "ips.pools.ips.read",
                "ips.pools.ips.update",
                "ips.pools.read",
                "ips.pools.update",
                "ips.read",
                "ips.warmup.create",
                "ips.warmup.delete",
                "ips.warmup.read",
                "ips.warmup.update",
                "mail_settings.address_whitelist.read",
                "mail_settings.address_whitelist.update",
                "mail_settings.bounce_purge.read",
                "mail_settings.bounce_purge.update",
                "mail_settings.footer.read",
                "mail_settings.footer.update",
                "mail_settings.forward_bounce.read",
                "mail_settings.forward_bounce.update",
                "mail_settings.forward_spam.read",
                "mail_settings.forward_spam.update",
                "mail_settings.plain_content.read",
                "mail_settings.plain_content.update",
                "mail_settings.read",
                "mail_settings.template.read",
                "mail_settings.template.update",
                "mail.batch.create",
                "mail.batch.delete",
                "mail.batch.read",
                "mail.batch.update",
                "mail.send",
                "mailbox_providers.stats.read",
                "marketing_campaigns.create",
                "marketing_campaigns.delete",
                "marketing_campaigns.read",
                "marketing_campaigns.update",
                "partner_settings.new_relic.read",
                "partner_settings.new_relic.update",
                "partner_settings.read",
                "stats.global.read",
                "stats.read",
                "subusers.create",
                "subusers.credits.create",
                "subusers.credits.delete",
                "subusers.credits.read",
                "subusers.credits.remaining.create",
                "subusers.credits.remaining.delete",
                "subusers.credits.remaining.read",
                "subusers.credits.remaining.update",
                "subusers.credits.update",
                "subusers.delete",
                "subusers.monitor.create",
                "subusers.monitor.delete",
                "subusers.monitor.read",
                "subusers.monitor.update",
                "subusers.read",
                "subusers.reputations.read",
                "subusers.stats.monthly.read",
                "subusers.stats.read",
                "subusers.stats.sums.read",
                "subusers.summary.read",
                "subusers.update",
                "suppression.blocks.create",
                "suppression.blocks.delete",
                "suppression.blocks.read",
                "suppression.blocks.update",
                "suppression.bounces.create",
                "suppression.bounces.delete",
                "suppression.bounces.read",
                "suppression.bounces.update",
                "suppression.create",
                "suppression.delete",
                "suppression.invalid_emails.create",
                "suppression.invalid_emails.delete",
                "suppression.invalid_emails.read",
                "suppression.invalid_emails.update",
                "suppression.read",
                "suppression.spam_reports.create",
                "suppression.spam_reports.delete",
                "suppression.spam_reports.read",
                "suppression.spam_reports.update",
                "suppression.unsubscribes.create",
                "suppression.unsubscribes.delete",
                "suppression.unsubscribes.read",
                "suppression.unsubscribes.update",
                "suppression.update",
                "teammates.create",
                "teammates.read",
                "teammates.update",
                "teammates.delete",
                "templates.create",
                "templates.delete",
                "templates.read",
                "templates.update",
                "templates.versions.activate.create",
                "templates.versions.activate.delete",
                "templates.versions.activate.read",
                "templates.versions.activate.update",
                "templates.versions.create",
                "templates.versions.delete",
                "templates.versions.read",
                "templates.versions.update",
                "tracking_settings.click.read",
                "tracking_settings.click.update",
                "tracking_settings.google_analytics.read",
                "tracking_settings.google_analytics.update",
                "tracking_settings.open.read",
                "tracking_settings.open.update",
                "tracking_settings.read",
                "tracking_settings.subscription.read",
                "tracking_settings.subscription.update",
                "user.account.read",
                "user.credits.read",
                "user.email.create",
                "user.email.delete",
                "user.email.read",
                "user.email.update",
                "user.multifactor_authentication.create",
                "user.multifactor_authentication.delete",
                "user.multifactor_authentication.read",
                "user.multifactor_authentication.update",
                "user.password.read",
                "user.password.update",
                "user.profile.read",
                "user.profile.update",
                "user.scheduled_sends.create",
                "user.scheduled_sends.delete",
                "user.scheduled_sends.read",
                "user.scheduled_sends.update",
                "user.settings.enforced_tls.read",
                "user.settings.enforced_tls.update",
                "user.timezone.read",
                "user.username.read",
                "user.username.update",
                "user.webhooks.event.settings.read",
                "user.webhooks.event.settings.update",
                "user.webhooks.event.test.create",
                "user.webhooks.event.test.read",
                "user.webhooks.event.test.update",
                "user.webhooks.parse.settings.create",
                "user.webhooks.parse.settings.delete",
                "user.webhooks.parse.settings.read",
                "user.webhooks.parse.settings.update",
                "user.webhooks.parse.stats.read",
                "whitelabel.create",
                "whitelabel.delete",
                "whitelabel.read",
                "whitelabel.update"
            }}
            #endregion
            #region Alerts
            ,{ SendGridPermission.Alerts, new[]
            {
                "alerts.create",
                "alerts.delete",
                "alerts.read",
                "alerts.update"
            }}
            #endregion
            #region Api Keys
            ,{ SendGridPermission.ApiKeys, new[]
            {
                "api_keys.create",
                "api_keys.delete",
                "api_keys.read",
                "api_keys.update"
            }}
            #endregion
            #region ASM Groups
            ,{ SendGridPermission.ASMGroups, new[]
            {
                   "asm.groups.create",
                "asm.groups.delete",
                "asm.groups.read",
                "asm.groups.update"
            }}
            #endregion
            #region Billing
            ,{ SendGridPermission.Billing, new[]
            {
                "billing.create",
                "billing.delete",
                "billing.read",
                "billing.update"
            }}
            #endregion
            #region Categories
            ,{ SendGridPermission.Categories, new[]
            {
                "categories.create",
                "categories.delete",
                "categories.read",
                "categories.update",
                "categories.stats.read",
                "categories.stats.sums.read"
            }}
            #endregion
            #region Clients
            ,{ SendGridPermission.Clients, new[]
            {
                "clients.desktop.stats.read",
                "clients.phone.stats.read",
                "clients.stats.read",
                "clients.tablet.stats.read",
                "clients.webmail.stats.read"
            }}
            #endregion
            #region Credentials
            ,{ SendGridPermission.Credentials, new[]
            {
                "credentials.create",
                "credentials.delete",
                "credentials.read",
                "credentials.update"
            }}
            #endregion
            #region Domain Autnetication
            ,{ SendGridPermission.DomainAuthentication, new[]
            {
                "whitelabel.create",
                "whitelabel.delete",
                "whitelabel.read",
                "whitelabel.update"
            }}
            #endregion
            #region IPs
            ,{ SendGridPermission.IPs, new[]
            {
                "ips.assigned.read",
                "ips.read",
                "ips.pools.create",
                "ips.pools.delete",
                "ips.pools.read",
                "ips.pools.update",
                "ips.pools.ips.create",
                "ips.pools.ips.delete",
                "ips.pools.ips.read",
                "ips.pools.ips.update",
                "ips.warmup.create",
                "ips.warmup.delete",
                "ips.warmup.read",
                "ips.warmup.update"
            }}
            #endregion
            #region Mail
            ,{ SendGridPermission.Mail, new[]
			 {
                "mail.batch.create",
                "mail.batch.delete",
                "mail.batch.read",
                "mail.batch.update",
                "mail.send"
			}}
            #endregion
            #region Mail Settings
            ,{ SendGridPermission.MailSettings, new[]
			 {
                "mail_settings.address_whitelist.read",
                "mail_settings.address_whitelist.update",
                "mail_settings.bounce_purge.read",
                "mail_settings.bounce_purge.update",
                "mail_settings.footer.read",
                "mail_settings.footer.update",
                "mail_settings.forward_bounce.read",
                "mail_settings.forward_bounce.update",
                "mail_settings.forward_spam.read",
                "mail_settings.forward_spam.update",
                "mail_settings.template.read",
                "mail_settings.template.update"
			}}
            #endregion
            #region Marketing Campaigns
            ,{ SendGridPermission.MarketingCampaigns, new[]
			 {
                "marketing_campaigns.create",
                "marketing_campaigns.delete",
                "marketing_campaigns.read",
                "marketing_campaigns.update"
			}}
            #endregion
            #region Newsletter
            ,{ SendGridPermission.Newsletter, new[]
			 {
                "newsletter.create",
                "newsletter.delete",
                "newsletter.read",
                "newsletter.update"
			}}
            #endregion
            #region PartnerSettings
            ,{ SendGridPermission.PartnerSettings, new[]
			 {
                "partner_settings.new_relic.read",
                "partner_settings.new_relic.update",
                "partner_settings.read"
			}}
            #endregion
            #region Reverse DNS
            ,{ SendGridPermission.ReverseDNS, new[]
			 {
                "access_settings.activity.read",
                "access_settings.whitelist.create",
                "access_settings.whitelist.delete",
                "access_settings.whitelist.read",
                "access_settings.whitelist.update"
			}}
            #endregion
            #region Scheduled Sends
            ,{ SendGridPermission.ScheduledSends, new[]
			 {
                "user.scheduled_sends.create",
                "user.scheduled_sends.delete",
                "user.scheduled_sends.read",
                "user.scheduled_sends.update"
			}}
            #endregion
            #region Stats
            ,{ SendGridPermission.Stats, new[]
			 {
                "email_activity.read",
                "stats.read",
                "stats.global.read",
                "browsers.stats.read",
                "devices.stats.read",
                "geo.stats.read",
                "mailbox_providers.stats.read",
                "clients.desktop.stats.read",
                "clients.phone.stats.read",
                "clients.stats.read",
                "clients.tablet.stats.read",
                "clients.webmail.stats.read"
			}}
            #endregion
            #region Subusers
            ,{ SendGridPermission.Subusers, new[]
			 {
                "subusers.create",
                "subusers.delete",
                "subusers.read",
                "subusers.update",
                "subusers.credits.create",
                "subusers.credits.delete",
                "subusers.credits.read",
                "subusers.credits.update",
                "subusers.credits.remaining.create",
                "subusers.credits.remaining.delete",
                "subusers.credits.remaining.read",
                "subusers.credits.remaining.update",
                "subusers.monitor.create",
                "subusers.monitor.delete",
                "subusers.monitor.read",
                "subusers.monitor.update",
                "subusers.reputations.read",
                "subusers.stats.read",
                "subusers.stats.monthly.read",
                "subusers.stats.sums.read",
                "subusers.summary.read"
			}}
            #endregion
            #region Suppressions
            ,{ SendGridPermission.Suppressions, new[]
			 {
                "suppression.create",
                "suppression.delete",
                "suppression.read",
                "suppression.update",
                "suppression.bounces.create",
                "suppression.bounces.read",
                "suppression.bounces.update",
                "suppression.bounces.delete",
                "suppression.blocks.create",
                "suppression.blocks.read",
                "suppression.blocks.update",
                "suppression.blocks.delete",
                "suppression.invalid_emails.create",
                "suppression.invalid_emails.read",
                "suppression.invalid_emails.update",
                "suppression.invalid_emails.delete",
                "suppression.spam_reports.create",
                "suppression.spam_reports.read",
                "suppression.spam_reports.update",
                "suppression.spam_reports.delete",
                "suppression.unsubscribes.create",
                "suppression.unsubscribes.read",
                "suppression.unsubscribes.update",
                "suppression.unsubscribes.delete"
			}}
            #endregion
            #region Teammates
            ,{ SendGridPermission.Teammates, new[]
			 {
                "teammates.create",
                "teammates.read",
                "teammates.update",
                "teammates.delete"
			}}
            #endregion
            #region Templates
            ,{ SendGridPermission.Templates, new[]
			 {
                "templates.create",
                "templates.delete",
                "templates.read",
                "templates.update",
                "templates.versions.activate.create",
                "templates.versions.activate.delete",
                "templates.versions.activate.read",
                "templates.versions.activate.update",
                "templates.versions.create",
                "templates.versions.delete",
                "templates.versions.read",
                "templates.versions.update"
			}}
            #endregion
            #region Tracking
            ,{ SendGridPermission.Tracking, new[]
			 {
                "tracking_settings.click.read",
                "tracking_settings.click.update",
                "tracking_settings.google_analytics.read",
                "tracking_settings.google_analytics.update",
                "tracking_settings.open.read",
                "tracking_settings.open.update",
                "tracking_settings.read",
                "tracking_settings.subscription.read",
                "tracking_settings.subscription.update"
			}}
            #endregion
            #region User Settings
            ,{ SendGridPermission.UserSettings, new[]
			 {
                "user.account.read",
                "user.credits.read",
                "user.email.create",
                "user.email.delete",
                "user.email.read",
                "user.email.update",
                "user.multifactor_authentication.create",
                "user.multifactor_authentication.delete",
                "user.multifactor_authentication.read",
                "user.multifactor_authentication.update",
                "user.password.read",
                "user.password.update",
                "user.profile.read",
                "user.profile.update",
                "user.settings.enforced_tls.read",
                "user.settings.enforced_tls.update",
                "user.timezone.read",
                "user.timezone.update",
                "user.username.read",
                "user.username.update"
			}}
            #endregion
            #region Webhooks
            ,{ SendGridPermission.Webhook, new[]
			 {
                "user.webhooks.event.settings.read",
                "user.webhooks.event.settings.update",
                "user.webhooks.event.test.create",
                "user.webhooks.event.test.read",
                "user.webhooks.event.test.update",
                "user.webhooks.parse.settings.create",
                "user.webhooks.parse.settings.delete",
                "user.webhooks.parse.settings.read",
                "user.webhooks.parse.settings.update",
                "user.webhooks.parse.stats.read"
			}}
            #endregion
        };
    }
}
