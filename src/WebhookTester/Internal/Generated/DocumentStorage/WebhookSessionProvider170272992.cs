// <auto-generated/>
#pragma warning disable
using ApogeeDev.WebhookTester.Common.Models;
using Marten.Internal;
using Marten.Internal.Storage;
using Marten.Schema;
using Marten.Schema.Arguments;
using Npgsql;
using System;
using System.Collections.Generic;
using Weasel.Core;
using Weasel.Postgresql;

namespace Marten.Generated.DocumentStorage
{
    // START: UpsertWebhookSessionOperation170272992
    public class UpsertWebhookSessionOperation170272992 : Marten.Internal.Operations.StorageOperation<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly ApogeeDev.WebhookTester.Common.Models.WebhookSession _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertWebhookSessionOperation170272992(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }



        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Upsert;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Weasel.Postgresql.IGroupedParameterBuilder parameterBuilder, Weasel.Postgresql.ICommandBuilder builder, ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session)
        {
            builder.Append("select public.mt_upsert_webhooksession(");
            var parameter0 = parameterBuilder.AppendParameter(session.Serializer.ToJson(_document));
            parameter0.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            // .Net Class Type
            var parameter1 = parameterBuilder.AppendParameter(_document.GetType().FullName);
            parameter1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            var parameter2 = parameterBuilder.AppendParameter(document.Id);
            setVersionParameter(parameterBuilder);
            builder.Append(')');
        }

    }

    // END: UpsertWebhookSessionOperation170272992
    
    
    // START: InsertWebhookSessionOperation170272992
    public class InsertWebhookSessionOperation170272992 : Marten.Internal.Operations.StorageOperation<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly ApogeeDev.WebhookTester.Common.Models.WebhookSession _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertWebhookSessionOperation170272992(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }



        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Insert;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Weasel.Postgresql.IGroupedParameterBuilder parameterBuilder, Weasel.Postgresql.ICommandBuilder builder, ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session)
        {
            builder.Append("select public.mt_insert_webhooksession(");
            var parameter0 = parameterBuilder.AppendParameter(session.Serializer.ToJson(_document));
            parameter0.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            // .Net Class Type
            var parameter1 = parameterBuilder.AppendParameter(_document.GetType().FullName);
            parameter1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            var parameter2 = parameterBuilder.AppendParameter(document.Id);
            setVersionParameter(parameterBuilder);
            builder.Append(')');
        }

    }

    // END: InsertWebhookSessionOperation170272992
    
    
    // START: UpdateWebhookSessionOperation170272992
    public class UpdateWebhookSessionOperation170272992 : Marten.Internal.Operations.StorageOperation<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly ApogeeDev.WebhookTester.Common.Models.WebhookSession _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateWebhookSessionOperation170272992(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }



        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
            postprocessUpdate(reader, exceptions);
        }


        public override async System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            await postprocessUpdateAsync(reader, exceptions, token);
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Update;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Weasel.Postgresql.IGroupedParameterBuilder parameterBuilder, Weasel.Postgresql.ICommandBuilder builder, ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session)
        {
            builder.Append("select public.mt_update_webhooksession(");
            var parameter0 = parameterBuilder.AppendParameter(session.Serializer.ToJson(_document));
            parameter0.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            // .Net Class Type
            var parameter1 = parameterBuilder.AppendParameter(_document.GetType().FullName);
            parameter1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            var parameter2 = parameterBuilder.AppendParameter(document.Id);
            setVersionParameter(parameterBuilder);
            builder.Append(')');
        }

    }

    // END: UpdateWebhookSessionOperation170272992
    
    
    // START: QueryOnlyWebhookSessionSelector170272992
    public class QueryOnlyWebhookSessionSelector170272992 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<ApogeeDev.WebhookTester.Common.Models.WebhookSession>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyWebhookSessionSelector170272992(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ApogeeDev.WebhookTester.Common.Models.WebhookSession Resolve(System.Data.Common.DbDataReader reader)
        {

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = _serializer.FromJson<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<ApogeeDev.WebhookTester.Common.Models.WebhookSession> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = await _serializer.FromJsonAsync<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyWebhookSessionSelector170272992
    
    
    // START: LightweightWebhookSessionSelector170272992
    public class LightweightWebhookSessionSelector170272992 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>, Marten.Linq.Selectors.ISelector<ApogeeDev.WebhookTester.Common.Models.WebhookSession>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightWebhookSessionSelector170272992(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ApogeeDev.WebhookTester.Common.Models.WebhookSession Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = _serializer.FromJson<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<ApogeeDev.WebhookTester.Common.Models.WebhookSession> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = await _serializer.FromJsonAsync<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightWebhookSessionSelector170272992
    
    
    // START: IdentityMapWebhookSessionSelector170272992
    public class IdentityMapWebhookSessionSelector170272992 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>, Marten.Linq.Selectors.ISelector<ApogeeDev.WebhookTester.Common.Models.WebhookSession>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapWebhookSessionSelector170272992(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ApogeeDev.WebhookTester.Common.Models.WebhookSession Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = _serializer.FromJson<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<ApogeeDev.WebhookTester.Common.Models.WebhookSession> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = await _serializer.FromJsonAsync<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapWebhookSessionSelector170272992
    
    
    // START: DirtyTrackingWebhookSessionSelector170272992
    public class DirtyTrackingWebhookSessionSelector170272992 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>, Marten.Linq.Selectors.ISelector<ApogeeDev.WebhookTester.Common.Models.WebhookSession>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingWebhookSessionSelector170272992(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ApogeeDev.WebhookTester.Common.Models.WebhookSession Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = _serializer.FromJson<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<ApogeeDev.WebhookTester.Common.Models.WebhookSession> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ApogeeDev.WebhookTester.Common.Models.WebhookSession document;
            document = await _serializer.FromJsonAsync<ApogeeDev.WebhookTester.Common.Models.WebhookSession>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingWebhookSessionSelector170272992
    
    
    // START: QueryOnlyWebhookSessionDocumentStorage170272992
    public class QueryOnlyWebhookSessionDocumentStorage170272992 : Marten.Internal.Storage.QueryOnlyDocumentStorage<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyWebhookSessionDocumentStorage170272992(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyWebhookSessionSelector170272992(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: QueryOnlyWebhookSessionDocumentStorage170272992
    
    
    // START: LightweightWebhookSessionDocumentStorage170272992
    public class LightweightWebhookSessionDocumentStorage170272992 : Marten.Internal.Storage.LightweightDocumentStorage<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightWebhookSessionDocumentStorage170272992(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightWebhookSessionSelector170272992(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: LightweightWebhookSessionDocumentStorage170272992
    
    
    // START: IdentityMapWebhookSessionDocumentStorage170272992
    public class IdentityMapWebhookSessionDocumentStorage170272992 : Marten.Internal.Storage.IdentityMapDocumentStorage<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapWebhookSessionDocumentStorage170272992(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapWebhookSessionSelector170272992(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: IdentityMapWebhookSessionDocumentStorage170272992
    
    
    // START: DirtyTrackingWebhookSessionDocumentStorage170272992
    public class DirtyTrackingWebhookSessionDocumentStorage170272992 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingWebhookSessionDocumentStorage170272992(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertWebhookSessionOperation170272992
            (
                document, Identity(document),
                session.Versions.ForType<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(ApogeeDev.WebhookTester.Common.Models.WebhookSession document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingWebhookSessionSelector170272992(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: DirtyTrackingWebhookSessionDocumentStorage170272992
    
    
    // START: WebhookSessionBulkLoader170272992
    public class WebhookSessionBulkLoader170272992 : Marten.Internal.CodeGeneration.BulkLoader<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid> _storage;

        public WebhookSessionBulkLoader170272992(Marten.Internal.Storage.IDocumentStorage<ApogeeDev.WebhookTester.Common.Models.WebhookSession, System.Guid> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_webhooksession(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_webhooksession_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_webhooksession (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_webhooksession_temp.\"id\", mt_doc_webhooksession_temp.\"data\", mt_doc_webhooksession_temp.\"mt_version\", mt_doc_webhooksession_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_webhooksession_temp left join public.mt_doc_webhooksession on mt_doc_webhooksession_temp.id = public.mt_doc_webhooksession.id where public.mt_doc_webhooksession.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_webhooksession target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_webhooksession_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_webhooksession_temp (like public.mt_doc_webhooksession including defaults)";


        public override string CreateTempTableForCopying()
        {
            return CREATE_TEMP_TABLE_FOR_COPYING_SQL;
        }


        public override string CopyNewDocumentsFromTempTable()
        {
            return COPY_NEW_DOCUMENTS_SQL;
        }


        public override string OverwriteDuplicatesFromTempTable()
        {
            return OVERWRITE_SQL;
        }


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, ApogeeDev.WebhookTester.Common.Models.WebhookSession document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
        {
            await writer.WriteAsync(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar, cancellation);
            await writer.WriteAsync(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb, cancellation);
        }


        public override string MainLoaderSql()
        {
            return MAIN_LOADER_SQL;
        }


        public override string TempLoaderSql()
        {
            return TEMP_LOADER_SQL;
        }

    }

    // END: WebhookSessionBulkLoader170272992
    
    
    // START: WebhookSessionProvider170272992
    public class WebhookSessionProvider170272992 : Marten.Internal.Storage.DocumentProvider<ApogeeDev.WebhookTester.Common.Models.WebhookSession>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public WebhookSessionProvider170272992(Marten.Schema.DocumentMapping mapping) : base(new WebhookSessionBulkLoader170272992(new QueryOnlyWebhookSessionDocumentStorage170272992(mapping)), new QueryOnlyWebhookSessionDocumentStorage170272992(mapping), new LightweightWebhookSessionDocumentStorage170272992(mapping), new IdentityMapWebhookSessionDocumentStorage170272992(mapping), new DirtyTrackingWebhookSessionDocumentStorage170272992(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: WebhookSessionProvider170272992
    
    
}

