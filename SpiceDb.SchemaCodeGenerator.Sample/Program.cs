using Google.Protobuf.WellKnownTypes;

var roleAdmin = Role.Create("admins");
var rolePlebsManager = Role.Create("plebsJanitors");
var rolePlebs = Role.Create("plebs");

var userAdmin = User.Create("userA");
var userPleb = User.Create("userB");
var userPlebManager = User.Create("janitor");

var groupAdmin = Group.Create("groupAdmins");
var groupPlebs = Group.Create("groupPlebs");
var groupManagers = Group.Create("groupPlebs");

var when = new AuthorizationSchema.Caveat.when { from = Timestamp.FromDateTime(DateTime.UtcNow), to = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(10)) };

var groupAdminRoleBinding = RoleBinding.Create();
var plebsRoleBinding1 = RoleBinding.Create();
var plebsRoleBinding2 = RoleBinding.Create();
var plebsRoleBinding3 = RoleBinding.Create();

var roleBindings = new RoleBinding[]
{
    groupAdminRoleBinding,
    plebsRoleBinding1,
    plebsRoleBinding2,
    plebsRoleBinding3,
};

var relations_groups  = new[]
{
    AuthorizationSchema.user.Relation.group_group(groupAdmin.Id, userAdmin.Id),
    AuthorizationSchema.user.Relation.group_group(groupPlebs.Id, userPleb.Id),
    AuthorizationSchema.user.Relation.group_group(groupManagers.Id, userPlebManager.Id),
};

var relations_roleBindings = new []
{
    // Users in a role
    //      rolebinding:{rolebinding.id} # member # role:{role.Id}
    //      rolebinding:{rolebinding.id} # member # user:{user.Id}
    //
    // Have access to the resource
    //      group:{group.Id} # rolebinding # role:{role.ID}

    // give access to group: all admins in role admin
    AuthorizationSchema.rolebinding.Relation.user_group(groupAdminRoleBinding.Id, groupAdmin.Id),
    AuthorizationSchema.rolebinding.Relation.role_role(groupAdminRoleBinding.Id, roleAdmin.Id),
    AuthorizationSchema.group.Relation.rolebinding_rolebinding(groupAdmin.Id, groupAdminRoleBinding.Id, when),

    // give access to group: all admins in role admin
    AuthorizationSchema.rolebinding.Relation.user_group(plebsRoleBinding2.Id, groupAdmin.Id),
    AuthorizationSchema.rolebinding.Relation.role_role(plebsRoleBinding2.Id, rolePlebs.Id),
    AuthorizationSchema.group.Relation.rolebinding_rolebinding(groupPlebs.Id, plebsRoleBinding2.Id, when),

    // give access to group: single user in role
    AuthorizationSchema.rolebinding.Relation.user_user(plebsRoleBinding1.Id, userPlebManager.Id),
    AuthorizationSchema.rolebinding.Relation.role_role(plebsRoleBinding1.Id, rolePlebsManager.Id),
    AuthorizationSchema.group.Relation.rolebinding_rolebinding(groupPlebs.Id, plebsRoleBinding1.Id, when),

    // give access to group: all plebs in role pleb
    AuthorizationSchema.rolebinding.Relation.user_group(plebsRoleBinding2.Id, groupPlebs.Id),
    AuthorizationSchema.rolebinding.Relation.role_role(plebsRoleBinding2.Id, rolePlebs.Id),
    AuthorizationSchema.group.Relation.rolebinding_rolebinding(groupPlebs.Id, plebsRoleBinding2.Id, when),
};

var relations = new[]
{
    relations_groups,
    relations_roleBindings
};

var permission = AuthorizationSchema.user.Permission.user_read(userPleb.Id, userAdmin.Id);

record Role(
    string Id,
    string Name)
{
    public static Role Create(string name) => new(Guid.NewGuid().ToString(), name);
}

record RoleBinding(
    string Id)
{
    public static RoleBinding Create() => new(Guid.NewGuid().ToString());
}

record User(string Id, string Name)
{
    public static User Create(string name) => new(Guid.NewGuid().ToString(), name);
};

record Group(string Id, string Name)
{
    public static Group Create(string name) => new(Guid.NewGuid().ToString(), name);
};
