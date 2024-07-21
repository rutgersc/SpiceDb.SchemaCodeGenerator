// <auto-generated/>
#nullable enable
using System;
using Authzed.Api.V1;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

public static class AuthorizationSchema
{
    public static partial class user
    {
        public static ObjectReference ObjectRef(string id) =>
            new ObjectReference { ObjectType = "user", ObjectId = id.ToString() };

        public static SubjectReference SubjectRef(string id, string? optionalRelation = null) =>
            new SubjectReference { Object = ObjectRef(id), OptionalRelation = optionalRelation };

        public static SubjectReference SubjectRefWildcard() =>
            new SubjectReference { Object = new ObjectReference { ObjectType = "user", ObjectId = "*" } };

        public static class Relation
        {
            public enum Enum
            {
                group,
            }

            public static Relationship group_group(string id, string subjectId) =>
                new Relationship { Resource = ObjectRef(id), Relation = "group", Subject = AuthorizationSchema.group.SubjectRef(subjectId, "") };
        }

        public static class Permission
        {
            public static CheckPermissionRequest user_read(string objectId, SubjectReference subject) =>
                new() { Resource = user.ObjectRef(objectId), Permission = "user_read", Subject = subject };

            public static CheckPermissionRequest user_read(string objectId, string subjectId) =>
                new() { Resource = user.ObjectRef(objectId), Permission = "user_read", Subject = AuthorizationSchema.user.SubjectRef(subjectId) };

        }
    }
    public static partial class group
    {
        public static ObjectReference ObjectRef(string id) =>
            new ObjectReference { ObjectType = "group", ObjectId = id.ToString() };

        public static SubjectReference SubjectRef(string id, string? optionalRelation = null) =>
            new SubjectReference { Object = ObjectRef(id), OptionalRelation = optionalRelation };



        public static class Relation
        {
            public enum Enum
            {
                member,
                rolebinding,
            }

            public static Relationship member_user(string id, string subjectId) =>
                new Relationship { Resource = ObjectRef(id), Relation = "member", Subject = AuthorizationSchema.user.SubjectRef(subjectId, "") };

            public static Relationship rolebinding_rolebinding(string id, string subjectId, Caveat.when caveat) =>
                new Relationship { Resource = ObjectRef(id), Relation = "rolebinding", Subject = AuthorizationSchema.rolebinding.SubjectRef(subjectId, ""), OptionalCaveat = caveat.ToContextualizedCaveat() };
        }

        public static class Permission
        {
            public static CheckPermissionRequest membership(string objectId, SubjectReference subject) =>
                new() { Resource = group.ObjectRef(objectId), Permission = "membership", Subject = subject };

            public static CheckPermissionRequest membership(string objectId, string subjectId) =>
                new() { Resource = group.ObjectRef(objectId), Permission = "membership", Subject = AuthorizationSchema.user.SubjectRef(subjectId) };

            public static CheckPermissionRequest user_read(string objectId, SubjectReference subject) =>
                new() { Resource = group.ObjectRef(objectId), Permission = "user_read", Subject = subject };

            public static CheckPermissionRequest user_read(string objectId, string subjectId) =>
                new() { Resource = group.ObjectRef(objectId), Permission = "user_read", Subject = AuthorizationSchema.user.SubjectRef(subjectId) };

        }
    }
    public static partial class rolebinding
    {
        public static ObjectReference ObjectRef(string id) =>
            new ObjectReference { ObjectType = "rolebinding", ObjectId = id.ToString() };

        public static SubjectReference SubjectRef(string id, string? optionalRelation = null) =>
            new SubjectReference { Object = ObjectRef(id), OptionalRelation = optionalRelation };



        public static class Relation
        {
            public enum Enum
            {
                user,
                role,
            }

            public static Relationship user_user(string id, string subjectId) =>
                new Relationship { Resource = ObjectRef(id), Relation = "user", Subject = AuthorizationSchema.user.SubjectRef(subjectId, "") };

            public static Relationship user_group(string id, string subjectId) =>
                new Relationship { Resource = ObjectRef(id), Relation = "user", Subject = AuthorizationSchema.group.SubjectRef(subjectId, "") };

            public static Relationship role_role(string id, string subjectId) =>
                new Relationship { Resource = ObjectRef(id), Relation = "role", Subject = AuthorizationSchema.role.SubjectRef(subjectId, "") };
        }

        public static class Permission
        {
            public static CheckPermissionRequest user_read(string objectId, SubjectReference subject) =>
                new() { Resource = rolebinding.ObjectRef(objectId), Permission = "user_read", Subject = subject };

            public static CheckPermissionRequest user_read(string objectId, string subjectId) =>
                new() { Resource = rolebinding.ObjectRef(objectId), Permission = "user_read", Subject = AuthorizationSchema.user.SubjectRef(subjectId) };

        }
    }
    public static partial class role
    {
        public static ObjectReference ObjectRef(string id) =>
            new ObjectReference { ObjectType = "role", ObjectId = id.ToString() };

        public static SubjectReference SubjectRef(string id, string? optionalRelation = null) =>
            new SubjectReference { Object = ObjectRef(id), OptionalRelation = optionalRelation };



        public static class Relation
        {
            public enum Enum
            {
                user_read,
            }

            public static Relationship user_read_user_Wildcard(string id) =>
                new Relationship { Resource = ObjectRef(id), Relation = "user_read", Subject = AuthorizationSchema.user.SubjectRefWildcard() };
        }

        public static class Permission
        {

        }
    }
    public static partial class test
    {
        public static ObjectReference ObjectRef(Guid id) =>
            new ObjectReference { ObjectType = "test", ObjectId = id.ToString() };

        public static SubjectReference SubjectRef(Guid id, string? optionalRelation = null) =>
            new SubjectReference { Object = ObjectRef(id), OptionalRelation = optionalRelation };



        public static class Relation
        {
            public enum Enum
            {
                complicated_relation,
            }

            public static Relationship complicated_relation_user(Guid id, string subjectId, Caveat.when caveat) =>
                new Relationship { Resource = ObjectRef(id), Relation = "complicated_relation", Subject = AuthorizationSchema.user.SubjectRef(subjectId, ""), OptionalCaveat = caveat.ToContextualizedCaveat() };

            public static Relationship complicated_relation_group(Guid id, string subjectId, Caveat.when caveat) =>
                new Relationship { Resource = ObjectRef(id), Relation = "complicated_relation", Subject = AuthorizationSchema.group.SubjectRef(subjectId, "membership"), OptionalCaveat = caveat.ToContextualizedCaveat() };
        }

        public static class Permission
        {

        }
    }

    public static class Caveat
    {
        public class when
        {
            public Timestamp from { get; set; }
            public Timestamp now { get; set; }
            public Timestamp to { get; set; }

            public ContextualizedCaveat ToContextualizedCaveat()
            {
                var protoStruct = new Struct();
                if (this.from != null)
                {
                    protoStruct.Fields.Add("from", Value.ForString(this.from.ToString()));
                }
                if (this.now != null)
                {
                    protoStruct.Fields.Add("now", Value.ForString(this.now.ToString()));
                }
                if (this.to != null)
                {
                    protoStruct.Fields.Add("to", Value.ForString(this.to.ToString()));
                }

                return new ContextualizedCaveat()
                {
                    CaveatName = "when",
                    Context = protoStruct
                };
            }
        }
    }
}