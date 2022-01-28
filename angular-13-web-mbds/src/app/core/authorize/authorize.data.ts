import { AuthorizationAction } from './authorize.type';

export const authorizeMap: Map<string, string[]> = new Map([
    [
        'admin-dashboards', [ 'ROLE_ADMIN', 'ROLE_MODERATOR' ]
    ],
   /* [
        'dashboards', [ 'ROLE_ADMIN', 'ROLE_MODERATOR', 'ROLE_CLIENT' ]
    ],*/
    [
        'user-space', [ 'ROLE_ADMIN', 'ROLE_MODERATOR', 'ROLE_CLIENT' ]
    ],
    /*,
    [
        'apps', [ 'ROLE_ADMIN', 'ROLE_MODERATOR' ]
    ],
    [
        'pages', [ 'ROLE_ADMIN', 'ROLE_MODERATOR' ]
    ],
    [
        'user-interface', [ 'ROLE_ADMIN', 'ROLE_MODERATOR' ]
    ],
    [
        'documentation', [ 'ROLE_ADMIN', 'ROLE_MODERATOR' ]
    ],
    [
        'navigation-features', [ 'ROLE_ADMIN', 'ROLE_MODERATOR' ]
    ]*/
]);

export const authorizeActionUser: Map<string, AuthorizationAction> = new Map([
    [
        'ROLE_ADMIN', { read: true, update: true, create: true, delete: true}
    ],
    [
        'ROLE_MODERATOR', { read: true, update: true, create: false, delete: false}
    ]
]);



